using MessengerServicePublisher.Core.Entities;

namespace MessengerServicePublisher.Core.Interfaces
{
    public interface IBaseRepository
    {
        T GetById<T>(Guid id) where T : BaseEntity;
        List<T> List<T>() where T : BaseEntity;
        T Add<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
    }
}
