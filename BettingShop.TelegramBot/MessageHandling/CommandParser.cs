using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.MessageHandling
{
    public class CommandParser
    {
        public ICommandType ParseCommandType(string commandString)
        {
            var types = Assembly.GetCallingAssembly().GetTypes()
                .Where(T => T.IsClass)
                .Where(T => T.GetInterfaces().Contains(typeof(ICommandType)));
            foreach (var type in types)
            {
                var names = Attribute.GetCustomAttributes(type).OfType<AliasAttribute>().SelectMany(a => a.Aliases).Select(name => name.ToLowerInvariant());
                commandString = commandString.ToLowerInvariant();
                if (names.Contains(commandString))
                {
                    return (ICommandType)Activator.CreateInstance(type);
                }
            }
            return new UnknownCommandType();
        }
    }
}
