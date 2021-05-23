using System;
using System.ComponentModel.DataAnnotations;

namespace BetEvent.Api.Models
{
    public class BetMeta
    {
        [Required]
        public int BetSize { get; set; }

        [Required]
        public Guid EventId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
