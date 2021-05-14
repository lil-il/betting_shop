using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Executor.Executors;
using BettingShop.TelegramBot.Message;
using BettingShop.TelegramBot.User;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace BettingShop.TelegramBot
{
    public class BotHandler
    {
        private readonly ITelegramBotClient botClient;
        private ICommandTypeParser _typeParser;
        private UserMessageParser parser;
        private StateManager stateManager;
        private ExecutorsManager executorsManager;

        public BotHandler(ITelegramBotClient botClient, ICommandTypeParser typeParser, UserMessageParser parser)
        {
            this.botClient = botClient;
            this._typeParser = typeParser;
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
                    var noCommandExecutor = new NoCommandExecutor(botClient);
                    noCommandExecutor.ExecuteAsync(e.Message, new NoCommandCommandState());
                }
                else
                {
                    var state = stateManager.GetStateFromString(user.State);
                    var executor = executorsManager.GetExecutorFromState(user.State);
                    executor.ExecuteAsync(e.Message, state);
                }
            }
            else
            {
                var commandType = _typeParser.Parse(e.Message.Text);
                var executor = executorsManager.GetExecutorFromType(commandType);
                var state = stateManager.GetStateFromType(commandType);
                executor.ExecuteAsync(e.Message, state);
            }
        }

    }
}
