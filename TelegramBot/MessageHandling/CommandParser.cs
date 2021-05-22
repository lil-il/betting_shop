using System;
using System.Linq;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.MessageHandling
{
    public class CommandParser
    {
        public ICommandType ParseCommandType(string commandString)
        {
            var placeBetNames = Attribute.GetCustomAttributes(typeof(PlaceBetCommandType)).OfType<AliasAttribute>().SelectMany(a => a.Aliases).ToList();
            var createEventNames = Attribute.GetCustomAttributes(typeof(CreateEventCommandType)).OfType<AliasAttribute>().SelectMany(a => a.Aliases).ToList();
            if (placeBetNames.Contains(commandString))
                return new PlaceBetCommandType();
            if (createEventNames.Contains(commandString))
                return new CreateEventCommandType();
            throw new ArgumentException($"the command name {createEventNames} cannot be recognized");
        }
    }
}
