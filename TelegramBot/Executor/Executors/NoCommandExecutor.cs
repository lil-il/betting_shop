using System.Threading.Tasks;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
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

        public async Task ExecuteAsync(UserMessage message)
        {
            await client.SendTextMessageAsync(message.telegramMessage.Chat, $"Called NoCommandExecutor with message {message.telegramMessage.Text}");
        }
    }
}
