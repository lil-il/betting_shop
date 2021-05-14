using System.Threading.Tasks;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Commands;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor
{
    public interface IExecutor<T> 
        where T: ICommandType
    {
        public Task ExecuteAsync(Telegram.Bot.Types.Message message, TelegramBotClient client, ICommandState<T> state);
    }
}
