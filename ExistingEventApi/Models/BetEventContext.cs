using Microsoft.EntityFrameworkCore;

namespace ExistingEventApi.Models
{
    public class BetEventContext : DbContext
    {
        public BetEventContext(DbContextOptions<BetEventContext> options)
            : base(options)
        {
        }

        public DbSet<BetEvent> ExistingEvents { get; set; }
    }
}