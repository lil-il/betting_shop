using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command.Commands
{
    class UnknownCommandType : ICommandType
    {
        public string Name => "UnknownCommand";
    }
}
