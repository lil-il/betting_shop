using System;
using System.Collections.Generic;
using System.Text;

namespace EchoBotForTest
{
    [AttributeUsage(AttributeTargets.Class)]
    class AliasAttribute : Attribute
    {
        public string[] Aliases { get; }

        public AliasAttribute(params string[] aliases)
        {
            Aliases = aliases;
        }
    }
}
