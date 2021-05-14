using System;
using System.Threading.Tasks;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Command.Commands;
using Telegram.Bot;


namespace BettingShop.TelegramBot.Executor.Executors
{
    public class CreateEventExecutor : IExecutor<CreateEventCommandType>
    {
        private ITelegramBotClient client;
        public async Task ExecuteAsync(Telegram.Bot.Types.Message message, ICommandState<CreateEventCommandType> state)
        {
            throw new NotImplementedException();
        }

        public CreateEventExecutor(ITelegramBotClient botClient)
        {
            client = botClient;
        }
    }
}
