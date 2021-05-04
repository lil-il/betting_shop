using System;
using EchoBotForTest.User;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace EchoBotForTest
{
    public class BotHandler
    {
        private readonly ITelegramBotClient botClient;
        private readonly InputParser inputParser;
        public Action<UserRequest> OnMessage { get; set; }

        public BotHandler(ITelegramBotClient botClient, InputParser parser)
        {
            this.botClient = botClient;
            inputParser = parser;
        }

        public void Initialize()
        {
            botClient.OnMessage += OnMessageHandler;
            botClient.StartReceiving();
        }

        public void StopReciving()
        {
            botClient.StopReceiving();
        }

        private UserRequest ParseUserMessageText(IUser user, string message)
        {
            var userRequestType = inputParser.ParseUserMessage(message);
            return new UserRequest(user, userRequestType);
        }

        public void OnMessageHandler(object sender, MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.Text)
                return;
            var userRequest = ParseUserMessageText(new TelegramUser(e.Message.Chat.Id), e.Message.Text);
            OnMessage(userRequest);
        }
    }
}
