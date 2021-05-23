using System.Threading.Tasks;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    class UnknownCommandExecutor : IExecutor<UnknownCommandType>
    {
        private readonly ITelegramBotClient client;

        public UnknownCommandExecutor(ITelegramBotClient botClient)
        {
            client = botClient;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            await client.SendTextMessageAsync(message.telegramMessage.Chat, $"Я не знаю такой команды, введи /help, чтобы узнать, что я умею");
        }
    }
}
