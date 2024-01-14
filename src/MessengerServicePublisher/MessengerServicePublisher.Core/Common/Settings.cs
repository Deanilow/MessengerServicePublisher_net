using MessengerServicePublisher.Core.Interfaces;

namespace MessengerServicePublisher.Core.Common
{
    public class Settings : ISettings
    {
        public string GetDataFrom { get; set; }
        public string DeleteDataSendBd { get; set; }
        public string ClientIdGmail { get; set; }
        public string ClientSecretGmail { get; set; }
        public string NameProyectoGmail { get; set; }
        public string PermissionGmail { get; set; }
        public string SenderPhone { get; set; }
        public string SenderGmail { get; set; }
        public string Company { get; set; }
        public string Definition { get; set; }
        public string HostNameRabbitMQ { get; set; }
        public string UserNameRabbitMQ { get; set; }
        public string PasswordNameRabbitMQ { get; set; }
        public string Cycle { get; set; }
    }
}
