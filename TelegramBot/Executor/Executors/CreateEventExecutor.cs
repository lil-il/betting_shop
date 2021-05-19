using System;
using System.Threading.Tasks;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;


namespace BettingShop.TelegramBot.Executor.Executors
{
    public class CreateEventExecutor : IExecutor<CreateEventCommandType>
    {
        private readonly IUserCommandStateService stateService;
        private readonly ITelegramBotClient client;

        public CreateEventExecutor(ITelegramBotClient botClient, IUserCommandStateService stateService)
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
