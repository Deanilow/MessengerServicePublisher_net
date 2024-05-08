using MessengerServicePublisher.Core.Entities;
using MessengerServicePublisher.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessengerServicePublisher.Infrastructure.Data.Repositories
{
    public class CallPreviewsRepository : BaseRepository, ICallPreviewsRepository
    {
        public CallPreviewsRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<List<CallPreviews>> GetCallPreviewsByDefinition(string company, string definition) =>
           await _dbContext.CallPreviews.Where(x => company.Contains(x.Company.Trim()) && definition.Contains(x.Definition.Trim())).OrderBy(x => x.Id).ToListAsync();
        public async Task DeleteList(List<CallPreviews> callPreviews)
        {
            _dbContext.AttachRange(callPreviews);

            foreach (var entidad in callPreviews)
            {
                _dbContext.Entry(entidad).Property("Deleted").IsModified = true;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
