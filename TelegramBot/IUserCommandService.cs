using System;
using System.Collections.Generic;
using System.Linq;
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
            return userState.ContainsKey(user) ? userState[user] : null;
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
            var currentType = userState[user].GetType().GetInterface("ICommandState`1").GetGenericArguments().First();
            return (ICommandType) Activator.CreateInstance(currentType);
        }
    }
}
