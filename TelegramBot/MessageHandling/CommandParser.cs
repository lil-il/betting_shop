using System;
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
            //var helpNames = Attribute.GetCustomAttributes(typeof(HelpCommandType)).OfType<AliasAttribute>().SelectMany(a => a.Aliases).ToList();
            //var profileNames = Attribute.GetCustomAttributes(typeof(ProfileInfoCommandType)).OfType<AliasAttribute>().SelectMany(a => a.Aliases).ToList();
            //var placeBetNames = Attribute.GetCustomAttributes(typeof(PlaceBetCommandType)).OfType<AliasAttribute>().SelectMany(a => a.Aliases).ToList();
            //var createEventNames = Attribute.GetCustomAttributes(typeof(CreateEventCommandType)).OfType<AliasAttribute>().SelectMany(a => a.Aliases).ToList();
            //if (placeBetNames.Contains(commandString))
            //    return new PlaceBetCommandType();
            //if (createEventNames.Contains(commandString))
            //    return new CreateEventCommandType();
            //if (profileNames.Contains(commandString))
            //    return new ProfileInfoCommandType();
            //if (helpNames.Contains(commandString))
            //    return new HelpCommandType();
            //return new UnknownCommandType();


            var types = Assembly.GetCallingAssembly().GetTypes()
                .Where(T => T.IsClass)
                .Where(T => T.GetInterfaces().Contains(typeof(ICommandType)));
            foreach (var type in types)
            {
                var names = Attribute.GetCustomAttributes(type).OfType<AliasAttribute>().SelectMany(a => a.Aliases).ToList();
                if (names.Contains(commandString))
                {
                    return (ICommandType)Activator.CreateInstance(type);
                }
            }
            return new UnknownCommandType();
        }
    }
}
