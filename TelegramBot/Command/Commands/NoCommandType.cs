using System;
using System.Collections.Generic;
using System.Text;
using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command.Commands
{
    public class NoCommandType: ICommandType
    {
        public string Name => "NoCommand";
    }
}
