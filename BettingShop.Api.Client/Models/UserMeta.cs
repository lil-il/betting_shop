using System.ComponentModel.DataAnnotations;

namespace BettingShop.Api.Client.Models
{
    public class UserMeta
    {
        [Required]
        public long TelegramId { get; set; }

        [Required]
        public int Balance { get; set; }
    }
}