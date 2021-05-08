namespace EchoBotForTest.Command.Commands
{
    public enum CreateEventState
    {
        Name,
        Description,
        Lines,
        Deadline,
    }
    public class CreateEventCommandState : ICommandState<CreateEventCommandType>
    {
        public CreateEventState State { get; set; }
    }
}
