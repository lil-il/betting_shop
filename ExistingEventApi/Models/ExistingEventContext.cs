using Microsoft.EntityFrameworkCore;

namespace ExistingEventApi.Models
{
    public class ExistingEventContext : DbContext
    {
        public ExistingEventContext(DbContextOptions<ExistingEventContext> options)
            : base(options)
        {
        }

        public DbSet<ExistingEvent> ExistingEvents { get; set; }
    }
}
