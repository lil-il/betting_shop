namespace BettingShop.TelegramBot.Command.Commands
{
    public enum PlaceBetState
    { 
        EventNumber = 0,
        LineNumber = 1,
        OutcomeNumber = 2,
        BetAmount = 3,
    }
    public class PlaceBetCommandState : ICommandState<PlaceBetCommandType>
    {
        public PlaceBetState State { get; }

        public PlaceBetCommandState(PlaceBetState state)
        {
            State = state;
        }
    }
}
