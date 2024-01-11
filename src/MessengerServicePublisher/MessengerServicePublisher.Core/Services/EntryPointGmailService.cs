﻿using MassTransit.Transports;
using MessengerServicePublisher.Core.Entities;
using MessengerServicePublisher.Core.Helper;
using MessengerServicePublisher.Core.Interfaces;
using MessengerServicePublisher.Core.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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

                var listMessages = new List<Data>();

                var companySetting = _appSettings.Company.ToUpper();

                var BdSetting = bool.Parse(_appSettings.Bd.ToUpper() ?? "False");

                var DefinitionSetting = _appSettings.Definition.ToUpper();

                var SenderPhoneSetting = _appSettings.SenderPhone;

                switch (companySetting)
                {
                    case "PROSEGUR":
                        if (!BdSetting) listMessages = await GetMessagesImboxProsegurGmail(companySetting: companySetting, DefinitionSetting: DefinitionSetting, SenderPhoneSetting: SenderPhoneSetting);
                        break;
                    case "BIDASSOA":
                        if (!BdSetting) listMessages = await GetMessagesImboxBidassoaGmail(companySetting: companySetting, DefinitionSetting: DefinitionSetting, SenderPhoneSetting: SenderPhoneSetting);
                        if (BdSetting) listMessages = await GetMessagesBidassoaBd(companySetting: companySetting, DefinitionSetting: DefinitionSetting, SenderPhoneSetting: SenderPhoneSetting);
                        break;
                    default:
                        break;
                }

                if (listMessages != null && listMessages.Count > 0)
                {
                    for (int i = 0; i < listMessages.Count; i++)
                    {
                        var Queue = $"messagesPending-{listMessages[i].data.from}";

                        _logger.LogInformation($"Se Envia por RabbitMQ  a Queue : {Queue} y json : {System.Text.Json.JsonSerializer.Serialize(listMessages[i])}");

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

                            var stringContent = JsonConvert.SerializeObject(listMessages[i]);
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
        private async Task<List<Data>> GetMessagesImboxProsegurGmail(string companySetting, string DefinitionSetting, string SenderPhoneSetting)
        {
            try
            {
                List<Data> dataListMessage = new();

                List<string> arrayPhoneSenders = SenderPhoneSetting.Split(';').ToList();

                var service = GmailHelper.GetGmailService(applicationName: _appSettings.NameProyectoGmail, ClientId: _appSettings.ClientIdGmail, ClientSecret: _appSettings.ClientSecretGmail);

                var mailsGmail = service.GetMessages(query: $"from:{_appSettings.SenderGmail} is:unread", markRead: false, filterDefinitionTextBody: DefinitionSetting);

                _logger.LogInformation("Se obtuvo " + mailsGmail.Count() + " correos de Gmail");

                int indiceArray = 0;

                foreach (var objMessageGmail in mailsGmail)
                {
                    var subject = filterNumberText(objMessageGmail.subject);

                    var variablesBodyGmail = objMessageGmail.body.Split(';');

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

                    var cacheDataGmailSetting = _memoryCache.Get<IEnumerable<GmailSettings>>("ListGmailSetting");

                    if (cacheDataGmailSetting is null)
                    {
                        cacheDataGmailSetting = await repositoryGmailSettings.GetGmailSettingByCompanyAsync(companySetting);
                        _memoryCache.Set("ListGmailSetting", cacheDataGmailSetting);
                    }

                    var variable1Gmail = objArrayVariablesGmail.First().GetType().GetProperty("Value")?.GetValue(objArrayVariablesGmail.First())?.ToString();

                    var objGmailSetting = cacheDataGmailSetting.FirstOrDefault(x => x.Definition.ToUpper() == variable1Gmail);

                    foreach (var itemVariableGmail in objArrayVariablesGmail)
                    {
                        string variable = itemVariableGmail.GetType().GetProperty("Variable")?.GetValue(itemVariableGmail)?.ToString() ?? "";

                        string value = itemVariableGmail.GetType().GetProperty("Value")?.GetValue(itemVariableGmail)?.ToString() ?? "";

                        objGmailSetting.Description = objGmailSetting.Description.Replace(variable, value);
                    }

                    switch (objGmailSetting.Type)
                    {
                        case 1:
                            break;
                        case 2:
                            objGmailSetting.Description = ProcessMessageType2(objGmailSetting.Description);
                            break;
                        default:
                            break;
                    }

                    foreach (var item in subject.Split(';'))
                    {
                        var objData = new Data()
                        {
                            data = new MessagesModel()
                            {
                                to = "51917641085",
                                //to = item.ToString(),
                                from = arrayPhoneSenders[indiceArray],
                                messages = new List<MessagesDetailModel>()
                                {
                                        new MessagesDetailModel()
                                        {
                                        text = objGmailSetting.Description,
                                        fileUrl = "",
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
                            Status = "Pendiente",
                            MessagesDetail = JsonConvert.SerializeObject(objData.data.messages)
                        }
                        );

                        objData.data.id = objInsertResult.Id;

                        dataListMessage.Add(objData);
                    }
                    //Destribuye a los telefonos asignado en SenderPhoneSetting

                    indiceArray = (indiceArray + 1) % arrayPhoneSenders.Count;
                }

                return dataListMessage;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en GetMessagesImbox : " + ex.Message.ToString());
                return (List<Data>)Enumerable.Empty<Data>();
            }
        }

        private async Task<List<Data>> GetMessagesImboxBidassoaGmail(string companySetting, string DefinitionSetting, string SenderPhoneSetting)
        {
            try
            {
                List<Data> dataListMessage = new();

                var service = GmailHelper.GetGmailService(applicationName: _appSettings.NameProyectoGmail, ClientId: _appSettings.ClientIdGmail, ClientSecret: _appSettings.ClientSecretGmail);

                var mailsGmail = service.GetMessages(query: $"from:{_appSettings.SenderGmail} is:unread", markRead: true, filterDefinitionTextBody: DefinitionSetting);

                _logger.LogInformation("Se obtuvo " + mailsGmail.Count() + " correos de Gmail");

                foreach (var objMessageGmail in mailsGmail)
                {
                    using var scope = _serviceScopeFactoryLocator.CreateScope();

                    var repositoryMessages =
                       scope.ServiceProvider
                           .GetService<IMessagesRepository>();


                    foreach (var item in objMessageGmail.subject.Split(';'))
                    {
                        var objData = new Data()
                        {
                            data = new MessagesModel()
                            {
                                //to = "51900262844",
                                to = item.ToString(),
                                from = string.Empty,
                                messages = new List<MessagesDetailModel>() {
                                        new MessagesDetailModel()
                                        {
                                        text = objMessageGmail.body,
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
                            Status = "Pendiente",
                            MessagesDetail = JsonConvert.SerializeObject(objData.data.messages)
                        }
                        );

                        objData.data.id = objInsertResult.Id;

                        dataListMessage.Add(objData);
                    }
                }

                //var cantSenders = SenderPhoneSetting.Split(';');

                //if (cantSenders.Count() > 0) dataListMessage = DistribuirListado(dataListMessage, SenderPhoneSetting.Split(';'));

                return dataListMessage;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en GetMessagesImbox : " + ex.Message.ToString());
                return (List<Data>)Enumerable.Empty<Data>();
            }
        }
        private async Task<List<Data>> GetMessagesBidassoaBd(string companySetting, string DefinitionSetting, string SenderPhoneSetting)
        {
            try
            {
                List<Data> dataListMessage = new();

                List<string> arrayPhoneSenders = SenderPhoneSetting.Split(';').ToList();

                using var scope = _serviceScopeFactoryLocator.CreateScope();

                var repositoryMessages =
                      scope.ServiceProvider
                          .GetService<IMessagesRepository>();

                var repositoryMessagesPreviews =
                 scope.ServiceProvider
                     .GetService<IMessagesPreviewsRepository>();

                var listMessages = await repositoryMessagesPreviews.GetMessagesPreviewByDefinition(DefinitionSetting);

                _logger.LogInformation("Se obtuvo " + listMessages.Count() + " correos de BD bidassoa");

                var listMessagesWithSender = listMessages.Where(x => !string.IsNullOrEmpty(x.From)).ToList();

                var listMessagesWithNotSender = listMessages.Where(x => string.IsNullOrEmpty(x.From)).OrderBy(x => x.To).ToList();
                var distictToNumberMessages = listMessagesWithNotSender.DistinctBy(x => x.To).Select(x => x.To).ToList();
                int indiceArray = 0;

                foreach (var number in distictToNumberMessages)
                {
                    List<MessagesDetailModel> listMessagesAdd = new List<MessagesDetailModel>();

                    var listMessagesOnlyText = listMessagesWithNotSender.Where(x => x.To == number && string.IsNullOrEmpty(x.FileUrl));

                    foreach (var item in listMessagesOnlyText)
                    {
                        var objMessagesDetailModel = new MessagesDetailModel() 
                        { 
                            fileUrl = item.FileUrl,
                            text = item.Text,
                            order = item.Id,
                        };

                        listMessagesAdd.Add(objMessagesDetailModel);
                    }

                    var listMessagesOnlyFileUrl = listMessagesWithNotSender.Where(x => x.To == number && !string.IsNullOrEmpty(x.FileUrl));

                    foreach (var item in listMessagesOnlyFileUrl)
                    {
                        var objMessagesDetailModel = new MessagesDetailModel()
                        {
                            fileUrl = item.FileUrl,
                            text = item.Text,
                            order = item.Id,
                        };

                        listMessagesAdd.Add(objMessagesDetailModel);
                    }


                    var objData = new Data()
                    {
                        data = new MessagesModel()
                        {
                            to = number,
                            from = arrayPhoneSenders[indiceArray],
                            messages = listMessagesAdd.OrderBy(x=>x.order).ToList()
                        }
                    };

                    var objInsertResult = await repositoryMessages.Add(new Entities.Messages()
                    {
                        To = objData.data.to,
                        From = objData.data.from,
                        Company = companySetting,
                        Definition = DefinitionSetting,
                        Status = "Pendiente",
                        MessagesDetail = JsonConvert.SerializeObject(objData.data.messages)
                    }
                    );

                    objData.data.id = objInsertResult.Id;

                    dataListMessage.Add(objData);

                    indiceArray = (indiceArray + 1) % arrayPhoneSenders.Count;
                }

                return dataListMessage;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error en GetMessagesImbox : " + ex.Message.ToString());
                return (List<Data>)Enumerable.Empty<Data>();
            }
        }

        private static string ProcessMessageType2(string input)
        {
            // Dividir la cadena en fragmentos usando "||" como delimitador
            string[] fragments = input.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

            // Filtrar las secciones que contienen "(varx)"
            fragments = Array.FindAll(fragments, fragment => !Regex.IsMatch(fragment, @"\(\bvar[1-9]\b\)"));

            // Concatenar los fragmentos en un solo string
            string result = string.Join("", fragments);

            return result.Trim();
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
    }
}
