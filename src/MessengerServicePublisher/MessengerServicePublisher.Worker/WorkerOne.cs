using MessengerServicePublisher.Core.Interfaces;
using Pilgaard.BackgroundJobs;

namespace MessengerServicePublisher.Worker
{
    public class WorkerOne : IOneTimeJob
    {
        private static readonly SemaphoreSlim _taskSemaphore = new SemaphoreSlim(1, 1);

        private static readonly DateTime _utcNowAtStartup = DateTime.UtcNow;

        private readonly IEntryPointGmailService _entryPointService;
        public WorkerOne( IEntryPointGmailService entryPointService)
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
                await _entryPointService.ExecuteWorker();
            }
            finally
            {
                _taskSemaphore.Release();
            }
        }
        public DateTime ScheduledTimeUtc => _utcNowAtStartup.AddSeconds(3);
    }
}
