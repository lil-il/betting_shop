using EchoBotForTest.Command.Commands;
using EchoBotForTest.Executor.Executors;
using EchoBotForTest.Message;
using EchoBotForTest.User;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace EchoBotForTest
{
    public class BotHandler
    {
        private readonly TelegramBotClient botClient;
        private ICommandSerializer serializer;
        private UserMessageParser parser;
        private TelegramUser user;
        private StateManager stateManager;
        private ExecutorsManager executorsManager;

        public BotHandler(TelegramBotClient botClient, ICommandSerializer serializer, UserMessageParser parser)
        {
            this.botClient = botClient;
            this.serializer = serializer;
            this.parser = parser;
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
            userRequest.User = user;
            if (userRequest.Command == null)
            {
                if (user.State == "") 
                {
                    NoCommandExecutor.ExecuteAsync(e.Message, botClient, new NoCommandCommandState());
                }
                else
                {
                    var state = stateManager.GetStateFromString(user.State);
                    var executor = executorsManager.GetExecutorFromState(user.State);
                    executor.ExecuteAsync(e.Message, botClient, state);
                }
            }
            else
            {
                var commandType = serializer.Deserialize(e.Message.Text);
                var executor = executorsManager.GetExecutorFromType(commandType);
                var state = stateManager.GetStateFromType(commandType);
                executor.ExecuteAsync(e.Message, botClient, state);
            }
        }

    }
}
