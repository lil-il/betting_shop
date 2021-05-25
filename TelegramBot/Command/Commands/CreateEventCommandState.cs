namespace BettingShop.TelegramBot.Command.Commands
{
    public enum CreateEventState
    {
        Name = 0,
        Lines = 1,
        Outcomes = 2,
        Deadline = 3,
        Description = 4,
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
