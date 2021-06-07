using System.Collections.Generic;
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
            var betClient = new BetClient("http://localhost:27254");
            var userClient = new UserClient("http://localhost:27254");
            var myUser = await userClient.GetByTelegramIdAsync(message.TelegramMessage.From.Id);
            List<string> myUserBets = new List<string>();
            foreach (var betId in myUser.ParticipateBetsId.Split('\n'))
            {
               // myUserBets.Append(betClient.GetEventName(Guid.Parse(betId)));
            }

            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                $"Информация о профиле:\n" +
                $"Баланс: {myUser.Balance}\n" +
                $"Мои ставки: {myUser.ParticipateBetsId}");
        }
    }
}
