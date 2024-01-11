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
        public async Task<List<MessagesPreviews>> GetMessagesPreviewByDefinition(string definition) =>
           await _dbContext.MessagesPreviews.Where(x => definition.Contains(x.Definition.Trim())).ToListAsync();
    }
}
