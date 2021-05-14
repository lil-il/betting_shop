using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command.Commands
{
    [Alias("placcebet", "сделатьставку")]
    public class PlaceBetCommandType : ICommandType
    {
        public string Name => "PlaceBet";
    }
}
