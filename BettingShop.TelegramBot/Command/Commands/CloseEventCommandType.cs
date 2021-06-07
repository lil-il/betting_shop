using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command.Commands
{
    [Alias("closeevent", "закрытьсобытие")]
    public class CloseEventCommandType : ICommandType
    {
        public string Name => "CloseEvent";
    }
}
