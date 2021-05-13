using System;
using EchoBotForTest.Command;
using EchoBotForTest.Commands;

namespace EchoBotForTest
{
    public class StateManager
    {
        public ICommandState<ICommandType> GetStateFromString(string state)
        {
            throw new NotImplementedException();
        }

        public ICommandState<ICommandType> GetStateFromType(ICommandType type)
        {
            throw new NotImplementedException();
        }
    }
}
