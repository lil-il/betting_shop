using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command.Commands
{
    [Alias("start")]
    public class StartCommandType : ICommandType
    {
        public string Name => "Start";
    }
}
