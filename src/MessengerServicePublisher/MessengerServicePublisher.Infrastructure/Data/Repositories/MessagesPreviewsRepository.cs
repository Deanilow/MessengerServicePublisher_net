using MessengerServicePublisher.Core.Entities;
using MessengerServicePublisher.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessengerServicePublisher.Infrastructure.Data.Repositories
{
    public class MessagesPreviewsRepository : BaseRepository, IMessagesPreviewsRepository
    {
        public MessagesPreviewsRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<List<MessagesPreviews>> GetMessagesPreviewByDefinition(string company, string definition) =>
           await _dbContext.MessagesPreviews.Where(x => company.Contains(x.Company.Trim()) && definition.Contains(x.Definition.Trim())).OrderBy(x => x.Id).ToListAsync();
        public async Task DeleteList(List<MessagesPreviews> messagesPreviews)
        {
            _dbContext.AttachRange(messagesPreviews);

            foreach (var entidad in messagesPreviews)
            {
                _dbContext.Entry(entidad).Property("Deleted").IsModified = true;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
