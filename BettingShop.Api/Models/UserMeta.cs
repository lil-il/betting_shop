using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BettingShop.Api.Models
{
    public class UserMeta
    {
        [Required]
        public int Balance { get; set; }

        [Required]
        public string ParticipateBetsId { get; set; }
    }
}
