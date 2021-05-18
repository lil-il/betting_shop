using System;
using System.ComponentModel.DataAnnotations;

namespace BettingShop.Api.Client.Models
{
    public class BetEventMeta
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime BetDeadline { get; set; }
    }
}
