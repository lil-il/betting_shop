using System;
using System.Collections.Generic;
using System.Text;

namespace EchoBotForTest
{
    public interface IInputParser
    {
        public UserRequestType ParseUserMessage(string message);
    }
}
