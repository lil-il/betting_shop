
using System.Collections.Generic;
using ExistingEventApi.Models;


namespace WebApplicationDataBase
{
    public interface IEventRepository
    {
        IEnumerable<ExistingEvent> GetExistingEvents();
        ExistingEvent GetExistingEventById(int EventId);
        void CreateEvent(ExistingEvent Event);

        void UpdateEvent(ExistingEvent Event);
        void DeleteEvent(int Id);
    }
}
