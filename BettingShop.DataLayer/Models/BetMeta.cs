using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingShop.DataLayer.Models
{
    public class BetMeta
    {
        [Required] public int BetSize { get; set; }

        [Required] public Guid EventId { get; set; }

        [Required] public Guid UserId { get; set; }

        [Required] public string Outcome { get; set; }
    }
}
