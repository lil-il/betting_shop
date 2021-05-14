namespace BettingShop.TelegramBot.Command.Commands
{
    public enum PlaceBetState
    {
        No = 0,
        EventNumber = 1,
        LineNumber = 2,
        BetAmount = 3,
    }
    public class PlaceBetCommandState : ICommandState<CreateEventCommandType>
    {
        public PlaceBetState State { get; }

        public PlaceBetCommandState(PlaceBetState state)
        {
            State = state;
        }
    }
}
