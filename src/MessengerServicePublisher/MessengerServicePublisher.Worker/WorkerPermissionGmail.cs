using MessengerServicePublisher.Core.Interfaces;
using Pilgaard.BackgroundJobs;

namespace MessengerServicePublisher.Worker
{
    public class WorkerPermissionGmail : IOneTimeJob
    {
        private readonly ISettings _appSettings;

        private static readonly DateTime _utcNowAtStartup = DateTime.UtcNow;

        private readonly IEntryPointGmailService _entryPointService;
        public WorkerPermissionGmail(ISettings appSettings, IEntryPointGmailService entryPointService)
        {
            _appSettings = appSettings;
            _entryPointService = entryPointService;
        }
        public async Task RunJobAsync(CancellationToken cancellationToken = default)
        {
            await _entryPointService.PermissonGmail();
        }

        public DateTime ScheduledTimeUtc => _utcNowAtStartup.AddSeconds(5);

    }
}
