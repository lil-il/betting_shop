using System.Threading.Tasks;
using BettingShop.Api.Client;
using BettingShop.Api.Client.Models;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    public class StartExecutor : IExecutor<StartCommandType>
    {
        private readonly ITelegramBotClient client;

        public StartExecutor(ITelegramBotClient botClient)
        {
            this.client = botClient;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            var userClient = new UserClient("http://localhost:27254");
            await userClient.CreateAsync(new UserMeta { Balance = 1000 });
            await client.SendTextMessageAsync(message.TelegramMessage.Chat,
                "Круто, что ты теперь с нами, можешь ввести /help, чтобы узнать, что я умею");
        }
    }
}