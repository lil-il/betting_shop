using System;
using System.Collections.Generic;
using System.Text;
using EchoBotForTest.User;

namespace EchoBotForTest
{
    public class UserRequest
    {
        public IUser User { get; }
        public UserRequestType RequestType { get; }

        public UserRequest(IUser user, UserRequestType userRequestType)
        {
            User = user;
            RequestType = userRequestType;
        }
    }
}
