using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EchoBotForTest.Executor
{
    public interface IExecutor
    {
        public void Execute(Message message, TelegramBotClient client);
    }
}
