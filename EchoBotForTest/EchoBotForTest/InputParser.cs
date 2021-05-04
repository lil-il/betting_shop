using System;
using System.Collections.Generic;
using System.Text;
using EchoBotForTest;
using EchoBotForTest.Command.Commands;
using EchoBotForTest.Commands;

namespace EchoBotForTest
{
    public class InputParser : IInputParser
    {
        public UserRequestType ParseUserMessage(string message)
        {
            UserRequestType userRequestType;
            switch (message)
            {
                case "/start":
                    userRequestType = UserRequestType.Start;
                    break;
               default:
                   userRequestType = UserRequestType.UnknownCommand;
                   break;
            }

            return userRequestType;
        }
    }
}
