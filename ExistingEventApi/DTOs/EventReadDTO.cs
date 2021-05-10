using System;

namespace ExistingEventApi.DTOs
{
    public class EventReadDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime BetDeadline { get; set; }
    }
}