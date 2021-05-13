using System.Threading.Tasks;
using EchoBotForTest.Command;
using EchoBotForTest.Commands;
using Telegram.Bot;

namespace EchoBotForTest.Executor
{
    public interface IExecutor<T> 
        where T: ICommandType
    {
        public Task ExecuteAsync(Telegram.Bot.Types.Message message, TelegramBotClient client, ICommandState<T> state);
    }
}
