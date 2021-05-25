﻿using System;
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
            var config = container.GetInstance<Config>();
            var token = config.Token;
            var commandTypeService = new UserCommandStateService();
            var executorsFactory = new ExecutorsFactory(container);
            var parser = new UserMessageParser();
            var commandParser = new CommandParser();
            //var telegramHandler = container.GetInstance<BotHandler>();

            var botClient = new TelegramBotClient(token);
            var telegramHandler = new BotHandler(
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
        private static ServiceContainer CreateContainer()
        {
            return new ServiceContainer();
        }

        private static void RegisterDependencies(ServiceContainer container)
        {
            var r = new StreamReader("C:/Users/iprok/bot/betting_shop/TelegramBot/configs/Config.json");
            var json = r.ReadToEnd();

            container.Register<CreateEventCommandType, CreateEventCommandType>();
            container.Register<NoCommandType, NoCommandType>();
            container.Register<PlaceBetCommandType, PlaceBetCommandType>();
            container.Register<ProfileInfoCommandType, ProfileInfoCommandType>();
            container.Register<UnknownCommandType, UnknownCommandType>();
            container.Register<HelpCommandType, HelpCommandType>();
            container.Register<ITelegramUser, TelegramUser>();
            container.Register<IUserCommandTypeService, UserCommandStateService>();
            container.Register<ITelegramBotClient>(sf => new TelegramBotClient(sf.GetInstance<Config>().Token));
            container.Register(sf => JsonConvert.DeserializeObject<Config>(json));
            container.Register<IUserCommandStateService, UserCommandStateService>();
            container.Register<UserCommandStateService, UserCommandStateService>();
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
            container.Register<BotHandler, BotHandler>();
            container.Register<ExecutorsFactory, ExecutorsFactory>();
            container.Register<UserMessageParser, UserMessageParser>();
            container.Register<CommandParser, CommandParser>();
        }
    }
}
