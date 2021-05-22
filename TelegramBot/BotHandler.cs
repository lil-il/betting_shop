using System.Collections.Generic;
using BettingShop.TelegramBot.Executor.Executors;
using BettingShop.TelegramBot.MessageHandling;
using BettingShop.TelegramBot.User;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace BettingShop.TelegramBot
{
    public class BotHandler
    {
        private readonly ITelegramBotClient botClient;
        private readonly UserMessageParser parser;
        private readonly ExecutorsFactory executorsFactory;
        private readonly IUserCommandTypeService commandTypeService;
        private readonly CommandParser commandParser;
        public BotHandler(ITelegramBotClient botClient, ExecutorsFactory factory, UserMessageParser parser, IUserCommandTypeService typeService, CommandParser commandParser)
        {
            this.botClient = botClient;
            this.executorsFactory = factory;
            this.parser = parser;
            this.commandTypeService = typeService;
            this.commandParser = commandParser;
        }

        public void Initialize()
        {
            botClient.OnMessage += OnMessageHandler;
            botClient.StartReceiving();
        }

        public void StopReceiving()
        {
            botClient.StopReceiving();
        }

        public void OnMessageHandler(object sender, MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.Text)
                return;
            var userRequest = parser.ParseMessage(e.Message.Text);
            userRequest.User = new TelegramUser(e.Message.From.Id);
            userRequest.telegramMessage = e.Message;
            if (userRequest.Command == null)
            {
                var commandType = commandTypeService.GetCurrentCommandType(userRequest.User);
                if (commandType.Name == "NoCommand")
                {
                    var noCommandExecutor = new NoCommandExecutor(botClient);
                    noCommandExecutor.ExecuteAsync(userRequest).GetAwaiter().GetResult();
                }
                else
                {
                    var executor = executorsFactory.GetExecutor(commandType);
                    executor.ExecuteAsync(userRequest).GetAwaiter().GetResult();
                }
            }
            else
            {
                var commandType = commandParser.ParseCommandType(userRequest.Command);
                var executor = executorsFactory.GetExecutor(commandType);
                executor.ExecuteAsync(userRequest).GetAwaiter().GetResult();
            }
        }

    }
}
