using System;
using System.Threading.Tasks;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Command.Commands;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    class PlaceBetExecutor : IExecutor<PlaceBetCommandType>
    {
        private ITelegramBotClient client;
        public async Task ExecuteAsync(Telegram.Bot.Types.Message message,  ICommandState<PlaceBetCommandType> state)
        {
            throw new NotImplementedException();
        }

        public PlaceBetExecutor(ITelegramBotClient botClient)
        {
            client = botClient;
        }
    }
}
