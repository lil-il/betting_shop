using System;
using System.Data;
using EchoBotForTest.Executor;
using EchoBotForTest.Message;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace EchoBotForTest
{
    public class BotHandler
    {
        private readonly ITelegramBotClient botClient;
        private ICommandSerializer serializer;
        private UserMessageParser parser;


        public BotHandler(ITelegramBotClient botClient,  ICommandSerializer serializer)
        {
            this.botClient = botClient;
            this.serializer = serializer;
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
            var userRequest = parser.ParseMessage( e.Message.Text);
            if (userRequest.Command != null)
            {
                //пойти спросить есть ли стэйт
                //если есть, то вызвать исполнение
                //если нет, то 
            }
            else
            {
                var commandType = serializer.Deserialize(e.Message.Text);
                var executor = GetExecutor(commandType);
                executor.ExecuteAsync(e.Message, botClient);
            }
        }
    }
}
