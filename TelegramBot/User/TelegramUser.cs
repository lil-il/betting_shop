using BettingShop.TelegramBot.Command.Commands;

namespace BettingShop.TelegramBot.User
{
    public class TelegramUser : ITelegramUser
    {
        public string State { get; set; } = "";//типа строка по которой будем смотреть какая команда сейчас в работе (какой из стэйтов нужно обрабатывать)
        public CreateEventCommandState createEventState { get; set; } = new CreateEventCommandState(CreateEventState.No);
        public PlaceBetCommandState placeBetState { get; set; } = new PlaceBetCommandState(PlaceBetState.No);
        public long Id { get; }
        
        public TelegramUser(long chatId)
        {
            Id = chatId;
        }
    }
}
