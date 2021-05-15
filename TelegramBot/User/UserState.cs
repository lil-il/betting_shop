using System.Collections.Generic;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.User
{
    public class UserState
    {
        public Dictionary<TelegramUser, ICommandState<ICommandType>> userState;
    }
}
