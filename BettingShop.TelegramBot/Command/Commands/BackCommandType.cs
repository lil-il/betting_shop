using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command.Commands
{
    [Alias("back")]
    public class BackCommandType : ICommandType
    {
        public string Name => "Back";
    }
}
