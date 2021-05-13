using System;
using System.Threading.Tasks;
using EchoBotForTest.Command;
using EchoBotForTest.Command.Commands;
using Telegram.Bot;


namespace EchoBotForTest.Executor.Executors
{
    public class CreateEventExecutor : IExecutor<CreateEventCommandType>
    {
        public async Task ExecuteAsync(Telegram.Bot.Types.Message message, TelegramBotClient client, ICommandState<CreateEventCommandType> state)
        {
            throw new NotImplementedException();
        }
    }
}
