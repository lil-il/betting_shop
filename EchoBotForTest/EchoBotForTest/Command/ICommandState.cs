using EchoBotForTest.Command.Commands;
using EchoBotForTest.Commands;

namespace EchoBotForTest.Command
{
    public interface ICommandState<T>
        where T: ICommandType
    {
    }
}
