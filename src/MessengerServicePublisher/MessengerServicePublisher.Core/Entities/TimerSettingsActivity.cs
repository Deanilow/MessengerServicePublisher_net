
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerServicePublisher.Core.Entities
{
    public class TimerSettingsActivity : BaseEntity
    {
        public Guid IdTimerSettings { get; set; }

        [ForeignKey("IdTimerSettings")] 
        public TimerSettings TimerSettings { get; set; }
    }
}
