using System;
using System.Collections.Generic;
using System.Linq;
using ExistingEventApi.Models;

namespace WebApplicationDataBase
{
    public class EventRepository : IEventRepository
    {
        private readonly DBSQLite context;

        public EventRepository(DBSQLite context)
        {
            this.context = context;
        }

        public IEnumerable<IExistingEvent> GetExistingEvents()
        {
            return context.GetAll();
        }

        public IExistingEvent GetExistingEventById(int id)
        {
            return context.GetAll().FirstOrDefault(ev => ev.Id == id);
        }

        public void CreateEvent(IExistingEvent existingEvent)
        {
            if (existingEvent == null)
                throw new ArgumentNullException(nameof(existingEvent));

            context.CreateEvent(existingEvent);
        }

        public void DeleteEvent(int studentID)
        {
            context.DeleteEvent(studentID);
        }

        public void UpdateEvent(IExistingEvent existingEvent)
        {
            context.UpdateEvent(existingEvent);
        }
    }
}
