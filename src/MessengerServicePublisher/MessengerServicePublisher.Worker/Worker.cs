using MessengerServicePublisher.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Pilgaard.BackgroundJobs;

namespace MessengerServicePublisher.Worker
{
    public class Worker : IRecurringJob
    {
        private readonly ILogger<Worker> _logger;
        private static readonly SemaphoreSlim _taskSemaphore = new SemaphoreSlim(1, 1);
        private readonly IEntryPointGmailService _entryPointService;
        public Worker(ILogger<Worker> logger, IEntryPointGmailService entryPointService)
        {
            _logger = logger;
            _entryPointService = entryPointService;
        }
        public async Task RunJobAsync(CancellationToken cancellationToken = default)
        {
            if (!_taskSemaphore.Wait(0))
            {
                _logger.LogInformation("El trabajo ya está en ejecución");
                return;
            }

            try
            {
                _logger.LogInformation("Iniciando el trabajo");

                await _entryPointService.ExecuteAsync();

                //await Task.Delay(TimeSpan.FromSeconds(20), cancellationToken);

                _logger.LogInformation("Trabajo principal ejecutado");
            }
            finally
            {
                _taskSemaphore.Release();
            }
        }

        public TimeSpan Interval => TimeSpan.FromSeconds(5);
    }
}
