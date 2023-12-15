using MessengerServicePublisher.Core.Entities;

namespace MessengerServicePublisher.Core.Interfaces
{
    public interface IGmailSettingRepository : IBaseRepository
    {
        Task<List<GmailSettings>> GetGmailSettingByCompanyAsync(string Company);
    }
}
