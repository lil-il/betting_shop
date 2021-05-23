using Microsoft.EntityFrameworkCore;

namespace BetEvent.Api.Models
{
    public class BetEventContext : DbContext
    {
        public BetEventContext(DbContextOptions<BetEventContext> options)
            : base(options)
        {
        }

        public DbSet<BetEvent> Events { get; set; }
    }
}