using System;


namespace BettingShop.DataLayer.Models
{
    public class Bet : BetMeta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
