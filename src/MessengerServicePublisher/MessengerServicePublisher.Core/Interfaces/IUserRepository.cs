using MessengerServicePublisher.Core.Entities;

namespace MessengerServicePublisher.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<Users> GetByEmailAsync(string email);
    }
}
