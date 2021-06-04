using Microsoft.EntityFrameworkCore;

namespace BettingShop.Api.Models
{
    public class BetContext: DbContext
    {
        public BetContext(DbContextOptions<BetContext> options)
            : base(options)
        { }

        public DbSet<Bet> Bets { get; set; }
    }
}
