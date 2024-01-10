using MessengerServicePublisher.Core.Interfaces;
using MessengerServicePublisher.Core.Model;

namespace MessengerServicePublisher.Infrastructure.Data.Repositories
{
    public class MessagesRepository : BaseRepository, IMessagesRepository
    {
        public MessagesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<MessagesModelBd>> GetMessagesBd()
        {
            //var messages = this._dbContext.Database.ExecuteSqlInterpolatedAsync($"UpdateStudentMark");

            //return messages;
            return null;
        }
    }
}
