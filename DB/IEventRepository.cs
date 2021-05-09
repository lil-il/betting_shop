using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DB
{
    public class ExistingEvent
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public ExistingEvent(string name)
        {
            Name = name;
            Id = _count;
            _count++;
        }

        public ExistingEvent(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        private static long _count = 1;
    }
    public interface IEventRepository
    {
        IEnumerable<ExistingEvent> GetExistingEvents();
        ExistingEvent GetExistingEventByID(int EventId);
        void CreateEvent(ExistingEvent existingEvent);

        void UpdateEvent(ExistingEvent existingEvent);
        void DeleteEvent(int Id);
        bool Save();

    }
}
