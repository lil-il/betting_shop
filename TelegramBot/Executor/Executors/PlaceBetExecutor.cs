using System;
using System.Threading.Tasks;
using EchoBotForTest.Command;
using EchoBotForTest.Command.Commands;

using Telegram.Bot;

namespace EchoBotForTest.Executor.Executors
{
    class PlaceBetExecutor : IExecutor<PlaceBetCommandType>
    {
        public async Task ExecuteAsync(Telegram.Bot.Types.Message message, TelegramBotClient client, ICommandState<PlaceBetCommandType> state)
        {
            throw new NotImplementedException();
        }
    }
}
