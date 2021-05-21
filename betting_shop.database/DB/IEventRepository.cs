
using System.Collections.Generic;
using ExistingEventApi.Models;


namespace WebApplicationDataBase
{
    public interface IEventRepository
    {
        IEnumerable<ExistingEvent> GetExistingEvents();
        ExistingEvent GetExistingEventById(int EventId);
        void Create(ExistingEvent Event);

        void Update(ExistingEvent Event);
        void Delete(int Id);
    }
}
