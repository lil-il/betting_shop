using System;
using System.Collections.Generic;
using EchoBotForTest;
using EchoBotForTest.Command.Commands;
using EchoBotForTest.Commands;
using Ninject;
using Telegram.Bot;

namespace Awesome
{
    internal static class Program
        {
            private static ITelegramBotClient botClient;
            private static BotHandler telegramHandler;
            private static StandardKernel container = new StandardKernel();

            static void Main()
            {
                var token = Config.Token;
                container.Bind<IInputParser>().To<InputParser>().InSingletonScope();
               

                botClient = new TelegramBotClient(token);

                telegramHandler = new BotHandler(
                    botClient,
                    container.Get<InputParser>());

                telegramHandler.Initialize();


                Console.WriteLine("Press any key to shutdown bot");
                Console.ReadKey();
                telegramHandler.StopReceiving();
            }
        }
    }
