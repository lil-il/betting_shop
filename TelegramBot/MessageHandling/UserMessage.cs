using System;
using BettingShop.TelegramBot.User;

namespace BettingShop.TelegramBot.Message
{
    public class UserMessage
    {
        public TelegramUser User { get; set; }
        public string Command { get; set; }
        public string Tail { get; set; }
        public Telegram.Bot.Types.Message telegramMessage { get; set; }

        public UserMessage(string command, string tail)
        {
            this.Command = command;
            this.Tail = tail;
        }
    }

}
