namespace MessengerServicePublisher.Core.Interfaces
{
    public interface IEntityChangedObserver
    {
        void EntityChanged(object entity);
    }
}
