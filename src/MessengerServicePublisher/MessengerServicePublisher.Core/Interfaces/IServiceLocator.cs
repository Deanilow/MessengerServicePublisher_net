using Microsoft.Extensions.DependencyInjection;

namespace MessengerServicePublisher.Core.Interfaces
{
    public interface IServiceLocator : IDisposable
    {
        IServiceScope CreateScope();
        T Get<T>();
    }
}
