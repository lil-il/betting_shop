using System;
using System.Threading.Tasks;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Command.Commands;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    public class NoCommandExecutor : IExecutor<NoCommandType>
    {
        private readonly ITelegramBotClient client;

        public NoCommandExecutor(ITelegramBotClient botClient)
        {
            client = botClient;
        }

        public async Task ExecuteAsync(Telegram.Bot.Types.Message message, ICommandState<NoCommandType> state)
        {
            await client.SendTextMessageAsync(message.Chat, $"Called NoCommandExecutor with message {message.Text}");
        }
    }
}
