using EchoBotForTest.Commands;


namespace EchoBotForTest.Command.Commands
{
    [Alias("start", "старт")]
    public class StartCommandType : ICommandType
    {
        public string Name => "Start";
    }
}
