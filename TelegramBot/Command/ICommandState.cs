using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.Command.Commands;

namespace BettingShop.TelegramBot.Command
{
    public interface ICommandState<T>
        where T: ICommandType
    {
    }
}
