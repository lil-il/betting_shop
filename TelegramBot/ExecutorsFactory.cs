using System;
using BettingShop.TelegramBot.Command;
using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.Executor;

namespace BettingShop.TelegramBot
{
    public class ExecutorsFactory
    {
        public IExecutor<ICommandType> GetExecutor(ICommandType commandType)
        {
            throw new NotImplementedException();
        }

        public IExecutor<ICommandType> GetExecutorFromState(ICommandState<ICommandType> state)
        {
            throw new NotImplementedException();
        }
    }
}
