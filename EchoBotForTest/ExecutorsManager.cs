using System;
using EchoBotForTest.Commands;
using EchoBotForTest.Executor;

namespace EchoBotForTest
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
