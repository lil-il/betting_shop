using System.Linq;
using System.Reflection;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.User;
using LightInject;

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
        ICommandType GetCurrentCommandType(ITelegramUser user);
    }

    public class UserService : IUserCommandStateService, IUserCommandTypeService
    {
        private ServiceContainer container;

        public UserService(ServiceContainer container)
        {
            this.container = container;
        }

        public ICommandState GetCurrentState(ITelegramUser user)
        { 
            return user.State;
        }

        public bool SaveState(ITelegramUser user, ICommandState state)
        {
            user.State = state;
            return true;
        }

        public bool DeleteState(ITelegramUser user)
        {
            user.State = null;
            return true;
        }

        public ICommandType GetCurrentCommandType(ITelegramUser user)
        {
            if (user.State == null)
                return new NoCommandType();
            return Assembly.GetCallingAssembly().GetTypes().Where(T => 
                T.GetInterfaces().Contains(typeof(ICommandState)) && T.Equals(user.State.GetType()))
                .First().GenericTypeArguments.Select(T => container.GetInstance(T)).Cast<ICommandType>().First();
        }
    }
}
