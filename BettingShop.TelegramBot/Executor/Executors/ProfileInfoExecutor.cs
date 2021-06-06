using System.Threading.Tasks;
using BettingShop.Api.Client;
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
            var userClient = new UserClient("http://localhost:27254");
            var myUser = await userClient.GetByTelegramIdAsync(message.TelegramMessage.From.Id);
            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                $"Информация о профиле:" +
                $"Баланс: {myUser.Balance}" +
                $"Мои ставки: {myUser.ParticipateBetsId}");
        }
    }
}
