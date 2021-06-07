using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
        public static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                cancellationTokenSource.Cancel(true);
            };

            var container = CreateContainer();
            RegisterDependencies(container);

            var telegramHandler = container.GetInstance<BotHandler>();
            telegramHandler.Initialize();

            while (!token.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(60), token);
            }

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
            container.Register<SimpleMessageCommandType>();
            container.Register<StartCommandType>();
            container.Register<UnknownCommandType>();
            container.Register<HelpCommandType>();
            container.Register<ITelegramUser, TelegramUser>();
            container.Register<IUserCommandTypeService>(sf => sf.GetInstance<UserCommandStateService>());
            container.Register<ITelegramBotClient>(sf => new TelegramBotClient(sf.GetInstance<Config>().Token));
            container.RegisterSingleton<TelegramBotClient>();
            container.RegisterSingleton(sf =>
                JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path.GetFullPath("configs/Config.json"))));
            container.Register<IUserCommandStateService>(sf => sf.GetInstance<UserCommandStateService>());
            container.RegisterSingleton<UserCommandStateService>();
            container.RegisterInstance(container);
            container.Register<IExecutor<CreateEventCommandType>, CreateEventExecutor>();
            container.Register<IExecutor<PlaceBetCommandType>, PlaceBetExecutor>();
            container.Register<IExecutor<ProfileInfoCommandType>, ProfileInfoExecutor>();
            container.Register<IExecutor<UnknownCommandType>, UnknownCommandExecutor>();
            container.Register<IExecutor<HelpCommandType>, HelpExecutor>();
            container.Register<IExecutor<SimpleMessageCommandType>, SimpleMessageExecutor>();
            container.Register<IExecutor<StartCommandType>, StartExecutor>();
            container.Register<CreateEventExecutor>();
            container.RegisterSingleton<PlaceBetExecutor>();
            container.Register<ProfileInfoExecutor>();
            container.Register<UnknownCommandExecutor>();
            container.Register<HelpExecutor>();
            container.Register<SimpleMessageExecutor>();
            container.Register<StartExecutor>();
            container.Register<BotHandler>();
            container.Register<ExecutorsFactory>();
            container.Register<UserMessageParser>();
            container.Register<CommandParser>();
        }
    }
}

