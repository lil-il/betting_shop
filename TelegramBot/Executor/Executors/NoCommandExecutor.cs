using System;
using System.Threading.Tasks;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Command.Commands;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    public class NoCommandExecutor : IExecutor<NoCommandType>
    {
        private ITelegramBotClient client;
        public async Task ExecuteAsync(Telegram.Bot.Types.Message message, ICommandState<NoCommandType> state)
        {
            throw new NotImplementedException();
        }

        public NoCommandExecutor(ITelegramBotClient botClient)
        {
            client = botClient;
        }
    }
}
