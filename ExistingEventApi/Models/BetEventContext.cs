using Microsoft.EntityFrameworkCore;

namespace BettingShop.Api.Models
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