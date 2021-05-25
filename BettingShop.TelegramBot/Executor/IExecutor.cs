using System.Threading.Tasks;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor
{
    public interface IExecutor
    {
        Task ExecuteAsync(UserMessage message);
    }

    public interface IExecutor<T>: IExecutor
        where T: ICommandType
    {
    }
}
