using EchoBotForTest.Commands;
using EchoBotForTest.Executor;

namespace EchoBotForTest.Message
{
    public interface ICommandSerializer
    {
        public ICommandType Deserialize(string message);
    }
}
