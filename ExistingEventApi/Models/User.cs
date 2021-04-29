using System;
using System.Collections.Generic;

namespace UserApi.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public int Balance { get; set; }

        public ICollection<ExistingEventApi.Models.ExistingEvent> EventsWithRates { get; set; }
    }
}
