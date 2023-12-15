namespace MessengerServicePublisher.Core.Entities
{
    public class Messages : BaseEntity
    {
        public string Company { get; set; }
        public string Definition { get; set; }
        public string SubjectGmail { get; set; }
        public string BodyGmail { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string MessagesDetail { get; set; }
        public string Status { get; set; }
    }
}
