using System.Linq;
using System.Reflection;
using BettingShop.TelegramBot.Command;

namespace BettingShop.TelegramBot.User
{
    public class TelegramUser : ITelegramUser
    {
        public long Id { get; }

        public TelegramUser(long chatId)
        {
            Id = chatId;
        }

        protected bool Equals(TelegramUser other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TelegramUser) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
