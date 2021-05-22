using BettingShop.TelegramBot.Message;

namespace BettingShop.TelegramBot.MessageHandling
{
    public class UserMessageParser
    {
        public UserMessage ParseMessage(string message)
        {
            message = message.Trim(' ');
            var firstSpaceIndex = message.IndexOf(' ');

            var result = new UserMessage();

            if (message[0] == '/')
            {
                result.Command = message.TrimStart('/');
                
                if (firstSpaceIndex > 0)
                {
                    result.Command = result.Command.Substring(0, firstSpaceIndex);
                }
                
                result.Tail = message.Substring(firstSpaceIndex + 1);
                return result;
            }

            result.Tail = message;
            return result;
        }
    }
}
