namespace EchoBotForTest.Command.Commands
{
    public enum CreateEventState
    {
        No = 0,
        Name = 1,
        Description = 2,
        Lines = 3,
        Deadline = 4,
    }
    public class CreateEventCommandState : ICommandState<CreateEventCommandType>
    {
        public CreateEventState State { get; set; }

        public CreateEventCommandState(CreateEventState state)
        {
            State = state;
        }
    }
}
