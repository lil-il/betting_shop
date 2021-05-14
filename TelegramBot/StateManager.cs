using System;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot
{
    public class StateManager
    {
        public ICommandState<ICommandType> GetStateFromString(string state)
        {
            throw new NotImplementedException();
        }

        public ICommandState<ICommandType> GetStateFromType(ICommandType type)
        {
            throw new NotImplementedException();
        }
    }
}
