using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiClient.Models
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
