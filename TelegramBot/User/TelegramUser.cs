using BettingShop.TelegramBot.Command.Commands;

namespace BettingShop.TelegramBot.User
{
    public class TelegramUser : ITelegramUser
    {
        public long Id { get; }
        
        public TelegramUser(long chatId)
        {
            Id = chatId;
        }
    }
}
