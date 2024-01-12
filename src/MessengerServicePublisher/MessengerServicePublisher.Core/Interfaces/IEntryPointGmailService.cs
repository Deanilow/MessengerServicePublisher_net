namespace MessengerServicePublisher.Core.Interfaces
{
    public interface IEntryPointGmailService
    {
        Task ExecuteWorker();
        Task PermissonGmail();
    }
}
