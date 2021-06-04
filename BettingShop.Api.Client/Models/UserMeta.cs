﻿using System.ComponentModel.DataAnnotations;

namespace BettingShop.Api.Client.Models
{
    public class UserMeta
    {
        [Required]
        public int Balance { get; set; }

        [Required]
        public string ParticipateBetsId { get; set; }
    }
}