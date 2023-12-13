using MessengerServicePublisher.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MessengerServicePublisher.Core.Services
{
    public sealed class ServiceScopeFactoryLocator : IServiceLocator
    {
        private readonly IServiceScopeFactory _factory;
        private IServiceScope? _scope;

        public ServiceScopeFactoryLocator(IServiceScopeFactory factory)
        {
            _factory = factory;
        }

        public T Get<T>()
        {
            CreateScope();

            return _scope.ServiceProvider.GetService<T>();
        }

        public IServiceScope CreateScope()
        {
            // if (_scope == null) comment this out to avoid {"Cannot access a disposed object.\r\nObject name: 'IServiceProvider'."}
            _scope = _factory.CreateScope();
            return _scope;
        }

        public void Dispose()
        {
            _scope?.Dispose();
            _scope = null;
        }
    }
}
