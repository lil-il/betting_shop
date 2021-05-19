using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.MessageHandling
{
    public class CommandParser
    {
        public ICommandType ParseCommandType(string commandString)
        {
            if (commandString == "Placebet")
                return new PlaceBetCommandType();
            else return new CreateEventCommandType();
        }
    }
}
