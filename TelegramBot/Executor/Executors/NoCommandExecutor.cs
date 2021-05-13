using System;
using System.Threading.Tasks;
using EchoBotForTest.Command;
using EchoBotForTest.Command.Commands;
using Telegram.Bot;

namespace EchoBotForTest.Executor.Executors
{
    public class NoCommandExecutor : IExecutor<NoCommandType>
    {
        public async Task ExecuteAsync(Telegram.Bot.Types.Message message, TelegramBotClient client, ICommandState<NoCommandType> state)
        {
            throw new NotImplementedException();
        }
    }
}
