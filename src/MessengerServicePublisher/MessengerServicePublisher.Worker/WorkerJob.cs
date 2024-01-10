using MessengerServicePublisher.Core.Interfaces;
using Pilgaard.BackgroundJobs;

namespace MessengerServicePublisher.Worker
{
    public class WorkerJob : IRecurringJob
    {
        private readonly ISettings _appSettings;

        private static readonly SemaphoreSlim _taskSemaphore = new SemaphoreSlim(1, 1);
        private readonly IEntryPointGmailService _entryPointService;
        public WorkerJob(ISettings appSettings, IEntryPointGmailService entryPointService)
        {
            _appSettings = appSettings;
            _entryPointService = entryPointService;
        }
        public async Task RunJobAsync (CancellationToken cancellationToken = default)
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

        public TimeSpan Interval => TimeSpan.FromSeconds(double.Parse(_appSettings.EverySecond));
    }
}
