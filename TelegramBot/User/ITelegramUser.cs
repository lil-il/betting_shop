using BettingShop.TelegramBot.Command.Commands;

namespace BettingShop.TelegramBot.User
{
    public interface ITelegramUser
    {
        public CreateEventCommandState createEventState { get; set; }
        public PlaceBetCommandState placeBetState { get; set; }
        public long Id { get; }
    }
}
