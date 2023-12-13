using MessengerServicePublisher.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace MessengerServicePublisher.Infrastructure.Logger
{
    public class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private ILogger<T> _logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception ex, string message, params object[] args)
        {
            _logger.LogError(ex, message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}
