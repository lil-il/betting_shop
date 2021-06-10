using System;

namespace BettingShop.DataLayer.Models
{
    public class BetEvent : BetEventMeta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
