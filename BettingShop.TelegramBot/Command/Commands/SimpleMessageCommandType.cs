using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command.Commands
{
    [Alias]
    public class SimpleMessageCommandType : ICommandType
    {
        public string Name => "SimpleMessage";
    }
}
