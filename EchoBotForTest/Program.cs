using System;
using EchoBotForTest.Message;
using Ninject;
using Telegram.Bot;

namespace EchoBotForTest
{
    internal static class Program
        {
            private static ITelegramBotClient botClient;
            private static BotHandler telegramHandler;
            private static ICommandSerializer serializer;
            private static UserMessageParser parser;
            private static StandardKernel container = new StandardKernel();

        static void Main()
        {
            var token = Config.Token;

            botClient = new TelegramBotClient(token);
            telegramHandler = new BotHandler(
                botClient,
                serializer,
                parser);

            telegramHandler.Initialize();

            Console.WriteLine("Press any key to shutdown bot");
            Console.ReadKey();
            telegramHandler.StopReceiving();
        }
    }
}
