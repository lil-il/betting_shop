using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EchoBotForTest.Commands
{
    public interface ICommand
    {
        public  string Name { get; set; }
    }
}
