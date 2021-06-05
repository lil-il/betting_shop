using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingShop.DataLayer.Models
{
    public class Bet : BetMeta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
