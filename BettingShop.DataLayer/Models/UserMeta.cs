﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingShop.DataLayer.Models
{
    public class UserMeta
    {
        [Required]
        public int Balance { get; set; }

        [Required]
        public string ParticipateBetsId { get; set; }
    }
}