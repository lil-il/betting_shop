using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EchoBotForTest.Command.Commands
{
    class Casino : EchoBotForTest.Commands.Command
    {
        public override string[] Names { get; set; } = {"Ты где колоду заряжал?", "Ty gde kolodu zaryazhal?"};
        public override async void Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat, "В киоске");
        }
    }
}
