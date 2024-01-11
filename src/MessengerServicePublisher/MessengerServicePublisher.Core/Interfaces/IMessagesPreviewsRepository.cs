using MessengerServicePublisher.Core.Entities;

namespace MessengerServicePublisher.Core.Interfaces
{
    public interface IMessagesPreviewsRepository : IBaseRepository
    {
        Task<List<MessagesPreviews>> GetMessagesPreviewByDefinition(string definition);
    }
}
