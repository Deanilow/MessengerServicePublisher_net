namespace MessengerServicePublisher.Core.Interfaces
{
    public interface ISettings
    {
        string GetDataFrom { get; set; }
        string DeleteDataSendBd { get; set; }
        string ClientIdGmail { get; set; }
        string ClientSecretGmail { get; set; }
        string NameProyectoGmail { get; set; }
        string PermissionGmail { get; set; }
        string MarkRead { get; set; }
        string SenderPhone { get; set; }
        string SenderGmail { get; set; }
        string Company { get; set; }
        string Definition { get; set; }
        string HostNameRabbitMQ { get; set; }
        string UserNameRabbitMQ { get; set; }
        string PasswordNameRabbitMQ { get; set; }
        string Cycle { get; set; }
        string SecondsWaitingAfterSendRabbitMQ { get; set; }
    }
}
