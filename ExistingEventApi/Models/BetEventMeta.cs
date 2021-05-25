using System;
using System.ComponentModel.DataAnnotations;

namespace BetEvent.Api.Models
{
    public class BetEventMeta
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime BetDeadline { get; set; }

        /*[Required]
        public List<string> EventOutcomes { get; set; }*/
    }
}
