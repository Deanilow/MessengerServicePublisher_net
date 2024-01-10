using MessengerServicePublisher.Core.Model;

namespace MessengerServicePublisher.Core.Interfaces
{
    public interface IMessagesRepository : IBaseRepository
    {
        Task<List<MessagesModelBd>> GetMessagesBd();
    }
}
