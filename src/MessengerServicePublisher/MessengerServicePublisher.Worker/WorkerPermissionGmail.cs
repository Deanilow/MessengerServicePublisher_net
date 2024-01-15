using MessengerServicePublisher.Core.Interfaces;
using Pilgaard.BackgroundJobs;

namespace MessengerServicePublisher.Worker
{
    public class WorkerPermissionGmail : IOneTimeJob
    {
        private static readonly DateTime _utcNowAtStartup = DateTime.UtcNow;

        private readonly IEntryPointGmailService _entryPointService;
        public WorkerPermissionGmail(IEntryPointGmailService entryPointService)
        {
            _entryPointService = entryPointService;
        }
        public async Task RunJobAsync(CancellationToken cancellationToken = default)
        {
            await _entryPointService.PermissonGmail();
        }

        public DateTime ScheduledTimeUtc => _utcNowAtStartup.AddSeconds(5);

    }
}
