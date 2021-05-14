using System;
using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.Executor;

namespace BettingShop.TelegramBot
{
    public class ExecutorsManager
    {
        public IExecutor<ICommandType> GetExecutorFromType(ICommandType type)
        {
            throw new NotImplementedException();
        }

        public IExecutor<ICommandType> GetExecutorFromState(string state)
        {
            throw new NotImplementedException();
        }
    }
}
