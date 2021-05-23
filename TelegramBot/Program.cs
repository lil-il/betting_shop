using System;
using System.IO;
using BettingShop.TelegramBot.Command.Commands;
using BettingShop.TelegramBot.Executor;
using BettingShop.TelegramBot.Executor.Executors;
using BettingShop.TelegramBot.MessageHandling;
using BettingShop.TelegramBot.User;
using LightInject;
using Newtonsoft.Json;
using Telegram.Bot;

namespace BettingShop.TelegramBot
{
    internal static class Program
    {
        private static ITelegramBotClient botClient;
        private static BotHandler telegramHandler;
        private static ExecutorsFactory executorsFactory;
        private static UserMessageParser parser;
        private static IUserCommandTypeService commandTypeService;
        private static ServiceContainer container;
        private static CommandParser commandParser;

        static void Main()
        {
            var r = new StreamReader("C:/Users/iprok/bot/betting_shop/TelegramBot/Config.json");
            var json = r.ReadToEnd();
            var config = JsonConvert.DeserializeObject<Config>(json);
            var token = config.Token;
            container = new ServiceContainer();
            commandTypeService = new UserService(container);
            executorsFactory = new ExecutorsFactory(container);
            parser = new UserMessageParser();
            commandParser = new CommandParser();

            container.Register<CreateEventCommandType, CreateEventCommandType>();
            container.Register<NoCommandType, NoCommandType>();
            container.Register<PlaceBetCommandType, PlaceBetCommandType>();
            container.Register<ProfileInfoCommandType, ProfileInfoCommandType>();
            container.Register<UnknownCommandType, UnknownCommandType>();
            container.Register<HelpCommandType, HelpCommandType>();
            container.Register<ITelegramUser, TelegramUser>();
            container.Register<IUserCommandTypeService, UserService>();
            container.Register<ITelegramBotClient>(sf => new TelegramBotClient(sf.GetInstance<Config>().Token));
            container.Register<Config>(sf => JsonConvert.DeserializeObject<Config>(json));
            container.Register<IUserCommandStateService, UserService>();
            container.Register<UserService, UserService>();
            container.Register<ServiceContainer, ServiceContainer>();
            container.Register<IExecutor<CreateEventCommandType>, CreateEventExecutor>();
            container.Register<IExecutor<PlaceBetCommandType>, PlaceBetExecutor>();
            container.Register<IExecutor<NoCommandType>, NoCommandExecutor>();
            container.Register<IExecutor<ProfileInfoCommandType>, ProfileInfoExecutor>();
            container.Register<IExecutor<UnknownCommandType>, UnknownCommandExecutor>();
            container.Register<IExecutor<HelpCommandType>, HelpExecutor>();
            container.Register<CreateEventExecutor, CreateEventExecutor>();
            container.Register<PlaceBetExecutor, PlaceBetExecutor>();
            container.Register<ProfileInfoExecutor, ProfileInfoExecutor>();
            container.Register<UnknownCommandExecutor, UnknownCommandExecutor>();
            container.Register<HelpExecutor, HelpExecutor>();

            botClient = new TelegramBotClient(token);
            telegramHandler = new BotHandler(
                botClient,
                executorsFactory,
                parser,
                commandTypeService,
                commandParser);

            telegramHandler.Initialize();

            Console.WriteLine("Press any key to shutdown bot");
            Console.ReadKey();
            telegramHandler.StopReceiving();
        }
    }
}
