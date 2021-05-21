using System;
using System.Collections.Generic;
using System.Linq;
using ExistingEventApi.Models;

namespace betting_shop.database.DB
{
    public class EventRepository : IEventRepository
    {
        private readonly DBSQLite context;

        public EventRepository(DBSQLite context)
        {
            this.context = context;
        }

        public IEnumerable<ExistingEvent> GetExistingEvents()
        {
            return context.GetAll();
        }

        public ExistingEvent GetExistingEventById(int id)
        {
            return context.GetAll().FirstOrDefault(ev => ev.Id == id);
        }

        public void Create(ExistingEvent existingEvent)
        {
            if (existingEvent == null)
                throw new ArgumentNullException(nameof(existingEvent));

            context.Create(existingEvent);
        }

        public void Delete(int studentID)
        {
            context.Delete(studentID);
        }

        public void Update(ExistingEvent existingEvent)
        {
            context.Update(existingEvent);
        }
    }
}
