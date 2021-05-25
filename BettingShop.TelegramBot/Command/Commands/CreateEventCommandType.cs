using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command.Commands
{
    [Alias("createevent", "создатьсобытие")]
    public class CreateEventCommandType : ICommandType
    {
        public string Name => "CreateEvent";
    }
}
