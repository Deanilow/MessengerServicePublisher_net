using MessengerServicePublisher.Core.Interfaces;
using Pilgaard.BackgroundJobs;

namespace MessengerServicePublisher.Worker
{
    public class WorkerOne : IOneTimeJob
    {
        private static readonly DateTime _utcNowAtStartup = DateTime.UtcNow;

        private readonly IEntryPointGmailService _entryPointService;
        public WorkerOne(IEntryPointGmailService entryPointService)
        {
            _entryPointService = entryPointService;
        }
        public async Task RunJobAsync(CancellationToken cancellationToken = default)
        {
            //await _entryPointService.ExecuteAsync();
        }

        public DateTime ScheduledTimeUtc => _utcNowAtStartup.AddMinutes(1);

    }
}
