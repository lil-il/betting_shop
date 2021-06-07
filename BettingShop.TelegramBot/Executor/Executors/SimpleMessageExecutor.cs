using System.Threading.Tasks;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    public class SimpleMessageExecutor : IExecutor<SimpleMessageCommandType>
    {
        private readonly ITelegramBotClient client;

        public SimpleMessageExecutor(ITelegramBotClient botClient)
        {
            this.client = botClient;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                $"Я тебя не понимаю, введи /help, чтобы узнать, что я умею");
        }
    }
}