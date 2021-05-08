using System;
using System.Threading.Tasks;
using EchoBotForTest.Commands;
using EchoBotForTest.Message;
using Telegram.Bot;

namespace EchoBotForTest.Executor
{
    public interface IExecutor<T> 
        where T: ICommandType
    {
        public Task ExecuteAsync(Telegram.Bot.Types.Message message, TelegramBotClient client);
    }
}
