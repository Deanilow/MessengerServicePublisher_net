using MessengerServicePublisher.Core.Interfaces;

namespace MessengerServicePublisher.Infrastructure.Data.Repositories
{
    public class MessagesRepository : BaseRepository, IMessagesRepository
    {
        public MessagesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
