using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command.Commands
{
    [Alias("help")]
    public class HelpCommandType : ICommandType
    {
        public string Name => "Help";
    }
}
