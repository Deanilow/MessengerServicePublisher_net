using MessengerServicePublisher.Core.Interfaces;
using Pilgaard.BackgroundJobs;

namespace MessengerServicePublisher.Worker
{
    public class Worker : IRecurringJob
    {
        private static readonly SemaphoreSlim _taskSemaphore = new SemaphoreSlim(1, 1);
        private readonly IEntryPointGmailService _entryPointService;
        public Worker( IEntryPointGmailService entryPointService)
        {
            _entryPointService = entryPointService;
        }
        public async Task RunJobAsync(CancellationToken cancellationToken = default)
        {
            if (!_taskSemaphore.Wait(0))
            {
                return;
            }

            try
            {
                await _entryPointService.ExecuteAsync();
            }
            finally
            {
                _taskSemaphore.Release();
            }
        }

        public TimeSpan Interval => TimeSpan.FromSeconds(5);
    }
}
