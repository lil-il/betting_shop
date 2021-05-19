using System.Text;

namespace BettingShop.TelegramBot.Message
{
    public class UserMessageParser
    {
        public UserMessage ParseMessage(string message)
        {
            string command = null;
            var tail = new StringBuilder();
            var splitedMessage = message.Split(' ');
            for (var i = 0; i < splitedMessage.Length; i++)
            {
                if (splitedMessage[i] == "Createevent" || splitedMessage[i] == "Placebet") //какой-то из команд
                {
                    command = splitedMessage[i];
                    continue;
                }
                tail.Append(splitedMessage[i]);
            }
            return new UserMessage(command, tail.ToString());
        }
    }
}
