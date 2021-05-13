using System;
using System.Collections.Generic;
using System.Text;
using EchoBotForTest.Commands;

namespace EchoBotForTest.Command.Commands
{
    public class NoCommandType: ICommandType
    {
        public string Name => "NoCommand";
    }
}
