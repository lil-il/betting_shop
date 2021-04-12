using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EchoBotForTest.Command.Commands
{
    class Bullshit : EchoBotForTest.Commands.Command
    {
        public override string[] Names { get; set; } = {"Ай фак ю буллщит", "Ай фак ю булщит"};
        public override async void Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat, "щит");
        }
    }
}
