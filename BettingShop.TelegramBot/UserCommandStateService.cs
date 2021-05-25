﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.User;

namespace BettingShop.TelegramBot
{
    public class UserCommandStateService : IUserCommandStateService, IUserCommandTypeService
    {
        private static readonly ConcurrentDictionary<ITelegramUser, ICommandState> userState =
            new ConcurrentDictionary<ITelegramUser, ICommandState>();

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
            userState.TryRemove(user, out _);
            return true;
        }

        public bool TryGetCurrentCommandType(ITelegramUser user, out ICommandType commandType)
        {
            if (!userState.ContainsKey(user))
            {
                commandType = new NoCommandType();
                return false;
            }

            commandType = (ICommandType)Activator.CreateInstance(userState[user].GetType().GetInterfaces().Where(T=>T.IsGenericType).First().GetGenericArguments().First());
            return true;
        }
    }
}