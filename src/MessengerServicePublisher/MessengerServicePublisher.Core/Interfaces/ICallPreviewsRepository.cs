using MessengerServicePublisher.Core.Entities;

namespace MessengerServicePublisher.Core.Interfaces
{
    public interface ICallPreviewsRepository : IBaseRepository
    {
        Task<List<CallPreviews>> GetCallPreviewsByDefinition(string company, string definition);
        Task DeleteList(List<CallPreviews> CallPreviews);
    }
}
