using BettingShop.TelegramBot.Command;

namespace BettingShop.TelegramBot.User
{
    public interface ITelegramUser
    {
        public long Id { get; }
        public ICommandState State { get; set; }
    }
}
