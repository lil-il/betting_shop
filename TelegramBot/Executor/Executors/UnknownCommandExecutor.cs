using System.Threading.Tasks;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    public class UnknownCommandExecutor : IExecutor<UnknownCommandType>
    {
        private readonly ITelegramBotClient client;

        public UnknownCommandExecutor(ITelegramBotClient botClient)
        {
            this.client = botClient;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            await client.SendTextMessageAsync(message.TelegramMessage.Chat, $"Я не знаю такой команды, введи /help, чтобы узнать, что я умею");
        }
    }
}
