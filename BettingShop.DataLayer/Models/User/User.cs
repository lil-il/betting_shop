using System;

namespace BettingShop.DataLayer.Models
{
    public class User : UserMeta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
