using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command
{
    public interface ICommandState { }

    public interface ICommandState<T> : ICommandState
        where T: ICommandType
    {
    }
}
