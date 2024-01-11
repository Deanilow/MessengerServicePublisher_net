using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerServicePublisher.Core.Entities
{
    public class MessagesPreviews : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Definition { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
        public string FileUrl { get; set; }
    }
}
