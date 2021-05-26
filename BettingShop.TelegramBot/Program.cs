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
        private static void Main()
        {
            var container = CreateContainer();
            RegisterDependencies(container);

            var telegramHandler = container.GetInstance<BotHandler>();
            telegramHandler.Initialize();

            Console.WriteLine("Press any key to shutdown bot");
            Console.ReadKey();
            telegramHandler.StopReceiving();
        }
        private static ServiceContainer CreateContainer()
        {
            return new ServiceContainer();
        }

        private static void RegisterDependencies(ServiceContainer container)
        {
            container.Register<CreateEventCommandType>();
            container.Register<PlaceBetCommandType>();
            container.Register<ProfileInfoCommandType>();
            container.Register<UnknownCommandType>();
            container.Register<HelpCommandType>();
            container.Register<ITelegramUser, TelegramUser>();
            container.Register<IUserCommandTypeService>(sf => sf.GetInstance<UserCommandStateService>());
            container.Register<ITelegramBotClient>(sf => new TelegramBotClient(sf.GetInstance<Config>().Token));
            container.RegisterSingleton<TelegramBotClient>();
            container.RegisterSingleton(sf => JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path.GetFullPath("configs/Config.json"))));
            container.Register<IUserCommandStateService>(sf=> sf.GetInstance<UserCommandStateService>()); 
            container.RegisterSingleton<UserCommandStateService>();
            container.RegisterInstance(container);
            container.Register<IExecutor<CreateEventCommandType>, CreateEventExecutor>();
            container.Register<IExecutor<PlaceBetCommandType>, PlaceBetExecutor>();
            container.Register<IExecutor<ProfileInfoCommandType>, ProfileInfoExecutor>();
            container.Register<IExecutor<UnknownCommandType>, UnknownCommandExecutor>();
            container.Register<IExecutor<HelpCommandType>, HelpExecutor>();
            container.Register<CreateEventExecutor>();
            container.Register<PlaceBetExecutor>();
            container.Register<ProfileInfoExecutor>();
            container.Register<UnknownCommandExecutor>();
            container.Register<HelpExecutor>();
            container.Register<BotHandler>();
            container.Register<ExecutorsFactory>();
            container.Register<UserMessageParser>();
            container.Register<CommandParser>();
        }
    }
}
