using MessengerServicePublisher.Core.Entities;

namespace MessengerServicePublisher.Core.Interfaces
{
    public interface IGmailSettingRepository : IBaseRepository
    {
        Task<List<GmailSetting>> GetGmailSettingByCompanyAsync(string Company);
    }
}
