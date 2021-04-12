using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EchoBotForTest.Command.Commands
{
    internal class GetMyId : EchoBotForTest.Commands.Command
    {
        public override string[] Names { get; set; } = {
            "myid", "Myid", "myId", "MyId", "myID", "MyID",
            "my id", "My id", "my Id", "My Id", "my ID", "My ID"
        };
        public override async void Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, $"Your id: {message.From.Id}");
        }
    }
}
