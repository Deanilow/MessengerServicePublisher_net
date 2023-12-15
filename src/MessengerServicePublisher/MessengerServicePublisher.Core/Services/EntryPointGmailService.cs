using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MassTransit.Transports;
using MessengerServicePublisher.Core.Entities;
using MessengerServicePublisher.Core.Interfaces;
using MessengerServicePublisher.Core.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MessengerServicePublisher.Core.Services
{
    public class EntryPointGmailService : IEntryPointGmailService
    {
        private readonly ISettings _appSettings;
        private readonly ILogger<EntryPointGmailService> _logger;
        private readonly IServiceLocator _serviceScopeFactoryLocator;
        private readonly IMemoryCache _memoryCache;
        public EntryPointGmailService(ISettings appSettings, ILogger<EntryPointGmailService> logger, IMemoryCache memoryCache, IServiceLocator serviceScopeFactoryLocator)
        {
            _appSettings = appSettings;
            _logger = logger;
            _memoryCache = memoryCache;
            _serviceScopeFactoryLocator = serviceScopeFactoryLocator;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                _logger.LogInformation("ExecuteAsync EntryPointGmailService");

                var listMessagesImbox = await GetMessagesImbox();

                if (listMessagesImbox != null && listMessagesImbox.Count > 0)
                {
                    var ListDistinctFromNumber = listMessagesImbox.GroupBy(x => x.data.from).Select(g => g.First()).ToList();

                    for (int i = 0; i < ListDistinctFromNumber.Count; i++)
                    {
                        var Queue = $"messagesPending-{ListDistinctFromNumber[i].data.from}";

                        _logger.LogInformation($"Se Envia por RabbitMQ  a Queue : {Queue} y json : {System.Text.Json.JsonSerializer.Serialize(ListDistinctFromNumber)}");

                        var factory = new ConnectionFactory()
                        {
                            HostName = _appSettings.HostNameRabbitMQ,
                            UserName = _appSettings.UserNameRabbitMQ,
                            Password = _appSettings.PasswordNameRabbitMQ
                        };
                        using (var connection = factory.CreateConnection())
                        using (var channel = connection.CreateModel())
                        {
                            channel.QueueDeclare(queue: Queue, durable: true, exclusive: false, autoDelete: false, arguments: new Dictionary<string, object> { { "x-queue-type", "quorum" } });

                            var stringContent = JsonConvert.SerializeObject(listMessagesImbox);
                            var body = Encoding.UTF8.GetBytes(stringContent);

                            channel.BasicPublish(exchange: "", routingKey: Queue, basicProperties: null, body: body);
                        }
                    }
                }

                _logger.LogInformation("FIN EntryPointGmailService");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("EXCEPTION EntryPointGmailService " + ex.Message.ToString());
            }
        }
        private async Task<List<Data>> GetMessagesImbox()
        {
            try
            {
                List<Data> dataListMessage = new();

                var companySetting = _appSettings.Company;

                var DefinitionSetting = _appSettings.Definition;

                var SenderPhoneSetting = _appSettings.SenderPhone;

                var serviceGmail = await GetConnection();

                var mailsGmail = await GetMessages(serviceGmail);

                _logger.LogInformation("Se obtuvo " + mailsGmail.Count() + " correos de Gmail");

                foreach (var objMessageGmail in mailsGmail)
                {
                    Message objMessage = serviceGmail.Users.Messages.Get("me", objMessageGmail.Id).Execute();

                    var subject = objMessage.Payload.Headers.FirstOrDefault(h => h.Name == "Subject")?.Value ?? "";

                    subject = filterNumberText(subject);

                    if (string.IsNullOrEmpty(subject))
                    {
                        MarkAsRead(serviceGmail, objMessageGmail);
                        break;
                    }

                    string textBodyGmail = GetBodyMessage(objMessage);

                    var variablesBodyGmail = textBodyGmail.Split(';');

                    if (variablesBodyGmail.Length <= 0)
                    {
                        MarkAsRead(serviceGmail, objMessageGmail);
                        break;
                    }

                    var objArrayVariablesGmail = new object[variablesBodyGmail.Length];

                    for (int i = 0; i < variablesBodyGmail.Length; i++)
                    {
                        objArrayVariablesGmail[i] = new
                        {
                            Variable = $"(var{i + 1})",
                            Value = variablesBodyGmail[i].Trim()
                        };
                    }
                    using var scope = _serviceScopeFactoryLocator.CreateScope();

                    var repositoryGmailSettings =
                        scope.ServiceProvider
                            .GetService<IGmailSettingRepository>();

                    var repositoryMessages =
                       scope.ServiceProvider
                           .GetService<IMessagesRepository>();

                    if (companySetting.Contains("Prosegur"))
                    {
                        var cacheDataGmailSetting = _memoryCache.Get<IEnumerable<GmailSettings>>("ListGmailSetting");

                        if (cacheDataGmailSetting is null)
                        {
                            cacheDataGmailSetting = await repositoryGmailSettings.GetGmailSettingByCompanyAsync(companySetting);
                            _memoryCache.Set("ListGmailSetting", cacheDataGmailSetting);
                        }

                        var variable1Gmail = objArrayVariablesGmail.First().GetType().GetProperty("Value")?.GetValue(objArrayVariablesGmail.First())?.ToString();

                        if (string.IsNullOrEmpty(variable1Gmail))
                        {
                            MarkAsRead(serviceGmail, objMessageGmail);
                            break;
                        }

                        var objGmailSetting = cacheDataGmailSetting.FirstOrDefault(x => x.Definition.ToUpper() == variable1Gmail);

                        if (objGmailSetting is null)
                        {
                            MarkAsRead(serviceGmail, objMessageGmail);
                            break;
                        }

                        foreach (var itemVariableGmail in objArrayVariablesGmail)
                        {
                            string variable = itemVariableGmail.GetType().GetProperty("Variable")?.GetValue(itemVariableGmail)?.ToString() ?? "";

                            string value = itemVariableGmail.GetType().GetProperty("Value")?.GetValue(itemVariableGmail)?.ToString() ?? "";

                            objGmailSetting.Description = objGmailSetting.Description.Replace(variable, value);
                        }

                        foreach (var item in subject.Split(';'))
                        {
                            var objData = new Data()
                            {
                                data = new MessagesModel()
                                {
                                    to = "51900262844",
                                    //to = item.ToString(),
                                    from = SenderPhoneSetting,
                                    messages = new List<MessagesDetailModel>() {
                                        new MessagesDetailModel()
                                        {
                                        text = objGmailSetting.Description,
                                        fileUrl = "",
                                        order = 1
                                     }
                                    }
                                }
                            };

                            var objInsertResult = await repositoryMessages.Add(new Messages()
                            {
                                To = objData.data.to,
                                From = objData.data.from,
                                Company = companySetting,
                                Definition = DefinitionSetting,
                                SubjectGmail = subject,
                                BodyGmail = textBodyGmail,
                                Status = "Pendiente",
                                MessagesDetail = JsonConvert.SerializeObject(objData.data.messages)
                            }
                            );

                            objData.data.id = objInsertResult.Id;

                            dataListMessage.Add(objData);
                        }
                    }
                    MarkAsRead(serviceGmail, objMessageGmail);
                }
                return dataListMessage;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en GetMessagesImbox : " + ex.Message.ToString());
                return (List<Data>)Enumerable.Empty<Data>();
            }
        }

        private static string filterNumberText(string texto)
        {
            // Separamos la cadena por ;.
            string[] subcadenas = texto.Split(';');

            // Filtramos y modificamos las subcadenas según los criterios especificados.
            string[] subcadenasModificadas = subcadenas
                .Where(subcadena => subcadena.Length == 9 && EsNumero(subcadena))  // Filtramos por longitud igual a 9 y es número.
                .Select(subcadena => "51" + subcadena)                             // Agregamos "51" al inicio de cada subcadena.
                .ToArray();

            // Unimos las subcadenas modificadas.
            string textoModificado = string.Join(";", subcadenasModificadas);

            return textoModificado;
        }

        private static bool EsNumero(string valor)
        {
            return int.TryParse(valor, out _);
        }

        private static string DownloadAttachments(GmailService service, List<MessagePart> partesMensaje, string idMensaje, string pathTemp)
        {
            string filePaths = "";

            foreach (var parte in partesMensaje)
            {
                if (!string.IsNullOrEmpty(parte.Filename))
                {
                    string fileName = Path.Combine(pathTemp, parte.Filename);

                    string attId = parte.Body.AttachmentId;

                    MessagePartBody attachPart = service.Users.Messages.Attachments.Get("me", idMensaje, attId).Execute();

                    var asdasd = attachPart.Data.Replace('-', '+').Replace('_', '/');

                    byte[] data = Convert.FromBase64String(asdasd);

                    using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    filePaths = filePaths + fileName + ";";
                }
            }

            return filePaths;
        }

        private static string GetBodyMessage(Message mensaje)
        {
            string body = "";

            if (mensaje.Payload.Parts != null)
            {
                foreach (var parte in mensaje.Payload.Parts)
                {
                    if (parte.MimeType == "multipart/alternative")
                    {
                        foreach (var parteInterna in parte.Parts)
                        {
                            if (parteInterna.MimeType == "text/plain" || parteInterna.MimeType == "text/html")
                            {
                                body = Base64Decode(parteInterna.Body.Data.Trim().Replace('-', '+').Replace('_', '/'));
                                break;
                            }
                        }
                    }
                    else if (parte.MimeType == "text/plain" || parte.MimeType == "text/html")
                    {
                        body = Base64Decode(parte.Body.Data.Trim().Replace('-', '+').Replace('_', '/'));
                        break;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(mensaje.Payload.Body?.Data))
            {
                body = Base64Decode(mensaje.Payload.Body.Data.Trim().Replace('-', '+').Replace('_', '/'));
            }

            return body;
        }
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        private static void MarkAsRead(GmailService service, Message mensaje)
        {
            ModifyMessageRequest request = new ModifyMessageRequest();
            request.RemoveLabelIds = new List<string>() { "UNREAD" };

            UsersResource.MessagesResource.ModifyRequest modRequest = service.Users.Messages.Modify(request, "me", mensaje.Id);
            //modRequest.Execute();
        }

        private async Task<IEnumerable<Message>> GetMessages(GmailService gmailService)
        {
            try
            {
                var request = gmailService.Users.Messages.List("me");

                request.Q = $"from:{_appSettings.SenderGmail}";

                request.Q += " is:unread";

                var response = await request.ExecuteAsync();

                if (response != null && response.Messages != null)
                {
                    return response.Messages;
                }

                return Enumerable.Empty<Message>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<GmailService> GetConnection()
        {
            string[] scopes = new string[] { GmailService.Scope.GmailModify };

            UserCredential credential;

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets()
                {
                    ClientId = _appSettings.ClientIdGmail,
                    ClientSecret = _appSettings.ClientSecretGmail
                },
                scopes,
                "user",
                CancellationToken.None,
                new FileDataStore("Gmail.Api.Auth.Store")).Result;

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _appSettings.NameProyectoGmail,
            });

            return service;
        }
    }

}
