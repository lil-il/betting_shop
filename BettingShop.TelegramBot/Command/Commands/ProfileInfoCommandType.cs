using BettingShop.TelegramBot.Commands;

namespace BettingShop.TelegramBot.Command.Commands
{
    [Alias("profileinfo", "информацияопрофиле")]
    public class ProfileInfoCommandType : ICommandType
    {
        public string Name => "ProfileInfo";
    }
}
