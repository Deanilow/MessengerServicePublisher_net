namespace MessengerServicePublisher.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Udpated { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? Deleted { get; set; }
        public string? DeletedBy { get; set; }
    }
}
