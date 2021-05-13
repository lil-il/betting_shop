
using System.Collections.Generic;
using ExistingEventApi.Models;


namespace WebApplicationDataBase
{
    public interface IEventRepository
    {
        IEnumerable<IExistingEvent> GetExistingEvents();
        IExistingEvent GetExistingEventById(int EventId);
        void CreateEvent(IExistingEvent Event);

        void UpdateEvent(IExistingEvent Event);
        void DeleteEvent(int Id);
    }
}
