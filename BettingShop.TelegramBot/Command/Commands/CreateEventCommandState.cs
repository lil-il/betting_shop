namespace BettingShop.TelegramBot.Command.Commands
{
    public enum CreateEventState
    {
        Name = 0,
        Outcomes = 1,
        Deadline = 2,
        Description = 3,
    }
    public class CreateEventCommandState : ICommandState<CreateEventCommandType>
    {
        public CreateEventState State { get; }
        public FormingEvent Forming { get; set; } = new FormingEvent();

        public CreateEventCommandState(CreateEventState state)
        {
            State = state;
        }
    }
}
