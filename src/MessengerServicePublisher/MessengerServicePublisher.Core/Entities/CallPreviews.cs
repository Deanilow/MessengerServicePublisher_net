using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerServicePublisher.Core.Entities
{
    public class CallPreviews : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Company { get; set; }
        public string Definition { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string FileUrl { get; set; }
        public string JsonRequest { get; set; }
        public string JsonResponse { get; set; }
    }
}
