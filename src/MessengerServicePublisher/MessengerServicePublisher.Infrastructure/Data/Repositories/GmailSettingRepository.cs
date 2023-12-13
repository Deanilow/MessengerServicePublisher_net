using MessengerServicePublisher.Core.Entities;
using MessengerServicePublisher.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessengerServicePublisher.Infrastructure.Data.Repositories
{
    public class GmailSettingRepository : BaseRepository, IGmailSettingRepository
    {
        public GmailSettingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<GmailSetting>> GetGmailSettingByCompanyAsync(string Company) =>
            await _dbContext.GmailSettings.Where(user => user.Company == Company).ToListAsync();
    }
}
