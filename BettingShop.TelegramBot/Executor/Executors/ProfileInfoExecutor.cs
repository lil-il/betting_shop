using System.Text;
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
            var betEventClient = new BetEventClient("http://localhost:27254");
            var betClient = new BetClient("http://localhost:27254");
            var userClient = new UserClient("http://localhost:27254");
            var myUser = await userClient.GetByTelegramIdAsync(message.TelegramMessage.From.Id);
            if (myUser == null)
            {
                await client.SendTextMessageAsync(message.TelegramMessage.Chat, "Ты еще не зарегистрирован, введи /start, чтобы это сделать");
                return;
            }
            var userBets = await betClient.AllBetsForUserAsync(message.TelegramMessage.From.Id);
            var outputMessage = new StringBuilder();
            outputMessage.Append($"Информация о профиле:\n" +
                                 $"Баланс: {myUser.Balance}\n" +
                                 $"Мои ставки:\n");
            foreach (var bet in userBets)
            {
                var myEvent = await betEventClient.GetAsync(bet.EventId);
                outputMessage.Append($"Название: {myEvent.Name}\n" +
                                     $"Выбранный исход: {bet.Outcome}\n" +
                                     $"Сумма ставки: {bet.BetSize}\n" +
                                     $"______________\n");

            }
            await client.SendTextMessageAsync(message.TelegramMessage.Chat, outputMessage.ToString());
        }
    }
}
