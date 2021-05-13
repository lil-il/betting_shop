using System;
using System.Collections.Generic;

namespace ExistingEventApi.Models
{
    public class IExistingEvent
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime BetDeadline { get; set; }
    }
}
