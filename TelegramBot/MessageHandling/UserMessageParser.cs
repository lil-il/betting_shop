using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BettingShop.TelegramBot.Commands;
using BettingShop.TelegramBot.Message;

namespace BettingShop.TelegramBot.MessageHandling
{
    public class UserMessageParser
    {
        public UserMessage ParseMessage(string message)
        {
            var commandNames = GetCommandNames();
            string command = null;
            var tail = new StringBuilder();
            var splitedMessage = message.Split(' ');
            foreach (var word in splitedMessage)
            {
                if (commandNames.Contains(word)) 
                {
                    command = word;
                    continue;
                }
                tail.Append(word);
            }
            return new UserMessage(command, tail.ToString());
        }

        public List<string> GetCommandNames()
        {
            var commandTypes = Assembly.GetCallingAssembly().GetTypes().Where(T =>
                T.GetInterfaces().Contains(typeof(ICommandType)));
            return commandTypes.SelectMany(com => com.GetCustomAttributes()).OfType<AliasAttribute>().SelectMany(a => a.Aliases).ToList();
        }
    }
}
