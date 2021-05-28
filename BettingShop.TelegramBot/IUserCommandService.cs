using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.User;

namespace BettingShop.TelegramBot
{
    public interface IUserCommandStateService
    {
        ICommandState GetCurrentState(ITelegramUser user);
        bool SaveState(ITelegramUser user, ICommandState state);
        bool DeleteState(ITelegramUser user);
    }

    public interface IUserCommandTypeService
    {
        bool TryGetCurrentCommandType(ITelegramUser user, out ICommandType commandType);
    
    }
}
