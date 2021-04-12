using System;
using System.Collections.Generic;
using EchoBotForTest;
using EchoBotForTest.Command.Commands;
using EchoBotForTest.Commands;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Awesome
{
    class Program
    {
        static TelegramBotClient botClient;
        private static List<Command> commands;
        static void Main()
        {
            botClient = new TelegramBotClient(Config.Token);
            commands = new List<Command>();
            commands.Add(new GetMyId());
            commands.Add(new Casino());
            commands.Add(new Bullshit());

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
                $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.ReadKey();

            botClient.StopReceiving();
        }

        static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat from {message.From.FirstName} {message.From.LastName} with text {message.Text}.");

                foreach (var command in commands)
                {
                    if (command.Contains(message.Text))
                        command.Execute(message, botClient);
                }
            }
        }
    }
}
