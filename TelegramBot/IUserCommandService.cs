using System.Collections.Generic;
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
        private static Dictionary<ITelegramUser, ICommandState> userState = new Dictionary<ITelegramUser, ICommandState>();

        public UserService(ServiceContainer container)
        {
            this.container = container;
        }

        public ICommandState GetCurrentState(ITelegramUser user)
        { 
            if (userState.ContainsKey(user))
                return userState[user];
            return null;
        }

        public bool SaveState(ITelegramUser user, ICommandState state)
        {
            userState[user] = state;
            return true;
        }

        public bool DeleteState(ITelegramUser user)
        {
            userState.Remove(user);
            return true;
        }

        public ICommandType GetCurrentCommandType(ITelegramUser user)
        {
            if (!userState.ContainsKey(user))
                return new NoCommandType();
            return Assembly.GetCallingAssembly().GetTypes().Where(T => 
                T.GetInterfaces().Contains(typeof(ICommandState)) && T.GetInterfaces().Length == 2 &&
                T.Equals(userState[user].GetType()))
                .First().GenericTypeArguments.Select(T => container.GetInstance(T)).Cast<ICommandType>().First();
        }
    }
}
