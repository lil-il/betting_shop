using System;
using BettingShop.TelegramBot.Message;
using Telegram.Bot;

namespace BettingShop.TelegramBot
{
    internal static class Program
        {
            private static ITelegramBotClient botClient;
            private static BotHandler telegramHandler;
            private static ICommandTypeParser _typeParser;
            private static UserMessageParser parser;

            static void Main()
        {
            var token = Config.Token;

            botClient = new TelegramBotClient(token);
            telegramHandler = new BotHandler(
                botClient,
                _typeParser,
                parser);

            telegramHandler.Initialize();

            Console.WriteLine("Press any key to shutdown bot");
            Console.ReadKey();
            telegramHandler.StopReceiving();
        }
    }
}
