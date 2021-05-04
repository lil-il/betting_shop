using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EchoBotForTest.Command.Commands
{
    internal class Start : EchoBotForTest.Commands.ICommand
    {
        public string Name { get; set; } = "Start";
    }
}
