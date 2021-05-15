using System;
using System.Threading.Tasks;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Command.Commands;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    class PlaceBetExecutor : IExecutor<PlaceBetCommandType>
    {
        private readonly ITelegramBotClient client;

        public PlaceBetExecutor(ITelegramBotClient botClient)
        {
            client = botClient;
        }

        public async Task ExecuteAsync(Telegram.Bot.Types.Message message,  ICommandState<PlaceBetCommandType> state)
        {
            await client.SendTextMessageAsync(message.Chat, $"Called CreateEventExecutor with message {message.Text} and state {state}");
        }

    }
}
