using System.Collections.Generic;

namespace ExistingEventApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public int Balance { get; set; }

        public List<ExistingEvent> EventsWithRates { get; set; }
    }
}
