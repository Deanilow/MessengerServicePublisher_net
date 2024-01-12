namespace MessengerServicePublisher.Core.Entities
{
    public class TimerSettings : BaseEntity
    {
        public string Company { get; set; }
        public string Definition { get; set; }
        public string DateStart { get; set; }
        public string HourStart { get; set; }
        public string HourMax { get; set; }
        public ICollection<TimerSettingsActivity> TimerSettingsActivity { get; set; }
    }
}
