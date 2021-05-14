using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.Executor;

namespace BettingShop.TelegramBot.Message
{
    public interface ICommandTypeParser
    {
        public ICommandType Parse(string message);
    }
}
