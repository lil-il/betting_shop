using System;

namespace ExistingEventApi.Models
{
    public class ExistingEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime BetDeadline { get; set; }
    }
}