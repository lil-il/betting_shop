using Microsoft.EntityFrameworkCore;

namespace BetEvent.Api.Models
{
    public class BetContext: DbContext
    {
        public BetContext(DbContextOptions<BetContext> options)
            : base(options)
        { }

        public DbSet<Bet> Bets { get; set; }
    }
}
