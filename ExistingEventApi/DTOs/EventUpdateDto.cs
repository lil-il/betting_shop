﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ExistingEventApi.DTOs
{
    public class EventUpdateDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime BetDeadline { get; set; }
    }
}
