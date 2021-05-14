using System;

namespace BettingShop.TelegramBot
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
