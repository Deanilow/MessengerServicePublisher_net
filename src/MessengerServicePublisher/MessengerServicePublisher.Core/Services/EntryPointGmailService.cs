using Google.Apis.Gmail.v1.Data;
using MassTransit.Transports;
using MessengerServicePublisher.Core.Entities;
using MessengerServicePublisher.Core.Helper;
using MessengerServicePublisher.Core.Interfaces;
using MessengerServicePublisher.Core.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
                _logger.LogInformation($"ExecuteAsync EntryPointGmailService {DateTime.Now.ToString("HH:mm:ss tt")}");

                var listMessagesImbox = await GetMessagesImbox();

                if (listMessagesImbox != null && listMessagesImbox.Count > 0)
                {
                    for (int i = 0; i < listMessagesImbox.Count; i++)
                    {
                        var Queue = $"messagesPending-{listMessagesImbox[i].data.from}";

                        _logger.LogInformation($"Se Envia por RabbitMQ  a Queue : {Queue} y json : {System.Text.Json.JsonSerializer.Serialize(listMessagesImbox)}");

                        //var factory = new ConnectionFactory()
                        //{
                        //    HostName = _appSettings.HostNameRabbitMQ,
                        //    UserName = _appSettings.UserNameRabbitMQ,
                        //    Password = _appSettings.PasswordNameRabbitMQ
                        //};
                        //using (var connection = factory.CreateConnection())
                        //using (var channel = connection.CreateModel())
                        //{
                        //    channel.QueueDeclare(queue: Queue, durable: true, exclusive: false, autoDelete: false, arguments: new Dictionary<string, object> { { "x-queue-type", "quorum" } });

                        //    var stringContent = JsonConvert.SerializeObject(listMessagesImbox);
                        //    var body = Encoding.UTF8.GetBytes(stringContent);

                        //    channel.BasicPublish(exchange: "", routingKey: Queue, basicProperties: null, body: body);
                        //}
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

                var service = GmailHelper.GetGmailService(_appSettings.NameProyectoGmail, _appSettings.ClientIdGmail, _appSettings.ClientSecretGmail);

                var mailsGmail = service.GetMessages(query: $"from:{_appSettings.SenderGmail} is:unread", markRead: true);

                _logger.LogInformation("Se obtuvo " + mailsGmail.Count() + " correos de Gmail");

                foreach (var objMessageGmail in mailsGmail)
                {
                    var subject = objMessageGmail.Payload.Headers.FirstOrDefault(x => x.Name == "Subject").Value ?? "";

                    subject = filterNumberText(subject);

                    if (string.IsNullOrEmpty(subject)) break;

                    string textBodyGmail = GmailHelper.GetBodyTextMessage(objMessageGmail);

                    var variablesBodyGmail = textBodyGmail.Split(';');

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

                        if (string.IsNullOrEmpty(variable1Gmail)) break;
                    
                        var objGmailSetting = cacheDataGmailSetting.FirstOrDefault(x => x.Definition.ToUpper() == variable1Gmail);

                        if (objGmailSetting is null) break;

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

                            var objInsertResult = await repositoryMessages.Add(new Entities.Messages()
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

        private static string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
