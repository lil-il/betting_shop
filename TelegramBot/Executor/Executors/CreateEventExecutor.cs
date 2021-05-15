using System;
using System.Threading.Tasks;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Command.Commands;
using Telegram.Bot;


namespace BettingShop.TelegramBot.Executor.Executors
{
    public class CreateEventExecutor : IExecutor<CreateEventCommandType>
    {
        private readonly ITelegramBotClient client;

        public CreateEventExecutor(ITelegramBotClient botClient)
        {
            client = botClient;
        }

        public async Task ExecuteAsync(Telegram.Bot.Types.Message message, ICommandState<CreateEventCommandType> state)
        {
            await client.SendTextMessageAsync(message.Chat, $"Called CreateEventExecutor with message {message.Text} and state {state}");
        }

    }
}
