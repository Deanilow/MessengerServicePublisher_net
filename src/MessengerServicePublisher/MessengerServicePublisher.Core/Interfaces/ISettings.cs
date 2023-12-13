namespace MessengerServicePublisher.Core.Interfaces
{
    public interface ISettings
    {
        string ClientIdGmail { get; set; }
        string ClientSecretGmail { get; set; }
        string NameProyectoGmail { get; set; }
        string SenderPhone { get; set; }
        string SenderGmail { get; set; }
        string PathTemp { get; set; }
        string Company { get; set; }
        string Definition { get; set; }
        string HostNameRabbitMQ { get; set; }
        string UserNameRabbitMQ { get; set; }
        string PasswordNameRabbitMQ { get; set; }
    }
}
