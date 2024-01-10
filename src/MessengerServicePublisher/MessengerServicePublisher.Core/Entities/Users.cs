namespace MessengerServicePublisher.Core.Entities
{
    public class SenderSettings : BaseEntity
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}
