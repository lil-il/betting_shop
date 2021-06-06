using System.ComponentModel.DataAnnotations;

namespace BettingShop.Api.Models
{
    public class UserMeta
    {
        [Required]
        public int TelegramId { get; set; }

        [Required]
        public int Balance { get; set; }

        [Required]
        public string ParticipateBetsId { get; set; }
    }
}
