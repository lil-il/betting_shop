using System.Threading.Tasks;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    public class HelpExecutor : IExecutor<HelpCommandType>
    {
        private readonly ITelegramBotClient client;

        public HelpExecutor(ITelegramBotClient botClient)
        {
            this.client = botClient;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                $"Вот список моих команд:\n" +
                $" - /placebet - сделать ставку\n" +
                $" - /createevent - предложить своё событие\n" +
                $" - /profileinfo - посмотреть информацию о профиле");
        }
    }
}