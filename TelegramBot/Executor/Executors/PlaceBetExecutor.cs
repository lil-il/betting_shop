using System.Threading.Tasks;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot.Executor.Executors
{
    class PlaceBetExecutor : IExecutor<PlaceBetCommandType>
    {
        private readonly IUserCommandStateService stateService;
        private readonly ITelegramBotClient client;

        public PlaceBetExecutor(ITelegramBotClient botClient, IUserCommandStateService stateService)
        {
            client = botClient;
            this.stateService = stateService;
        }

        public async Task ExecuteAsync(UserMessage message)
        {
            await client.SendTextMessageAsync(message.telegramMessage.Chat, $"Called CreateEventExecutor with message {message.telegramMessage.Text} " +
                                                                            $"and state {stateService.GetCurrentState(message.User)}");
        }

    }
}
