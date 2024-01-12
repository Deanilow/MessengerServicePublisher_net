
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerServicePublisher.Core.Entities
{
    public class TimerSettingsActivity : BaseEntity
    {
        public string Company { get; set; }
        public string Definition { get; set; }
        public string HourStart { get; set; }
        public string HourMax { get; set; }
        public Guid IdTimerSettings { get; set; }

        [ForeignKey("PadreId")] // Atributo ForeignKey
        public TimerSettings TimerSettings { get; set; }
    }
}
