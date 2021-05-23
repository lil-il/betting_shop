using System.Threading.Tasks;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    class ProfileInfoExecutor : IExecutor<ProfileInfoCommandType>
    {
        private readonly ITelegramBotClient client;

        public ProfileInfoExecutor(ITelegramBotClient botClient)
        {
            client = botClient;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            //вывод информации о профиле
            await client.SendTextMessageAsync(message.telegramMessage.Chat,
                $"Информация о профиле");
        }
    }
}
