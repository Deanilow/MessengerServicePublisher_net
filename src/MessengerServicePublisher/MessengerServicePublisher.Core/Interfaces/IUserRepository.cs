using MessengerServicePublisher.Core.Entities;

namespace MessengerServicePublisher.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<User> GetByEmailAsync(string email);
    }
}
