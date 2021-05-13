using EchoBotForTest.Commands;

namespace EchoBotForTest.Command.Commands
{
    [Alias("placcebet", "сделатьставку")]
    public class PlaceBetCommandType : ICommandType
    {
        public string Name => "CreateEvent";
    }
}
