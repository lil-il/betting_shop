using BettingShop.TelegramBot.Command;

namespace BettingShop.TelegramBot.User
{
    public class TelegramUser : ITelegramUser
    {
        public long Id { get; }
        public ICommandState State { get; set; }
        
        public TelegramUser(long chatId)
        {
            Id = chatId;
        }
    }
}
