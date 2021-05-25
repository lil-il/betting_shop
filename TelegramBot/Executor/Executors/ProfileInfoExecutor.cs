using System.Threading.Tasks;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    public class ProfileInfoExecutor : IExecutor<ProfileInfoCommandType>
    {
        private readonly ITelegramBotClient client;

        public ProfileInfoExecutor(ITelegramBotClient botClient)
        {
            this.client = botClient;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            //вывод информации о профиле
            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                $"Информация о профиле");
        }
    }
}
