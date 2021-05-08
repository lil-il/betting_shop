using EchoBotForTest.Commands;

namespace EchoBotForTest.Command.Commands
{
    [Alias("createevent", "создатьсобытие")]
    public class CreateEventCommandType : ICommandType
    {
        public string Name => "CreateEvent";
    }
}
