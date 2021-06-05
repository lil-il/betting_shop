using System.Data.SQLite;
using BettingShop.DataLayer.Models;

namespace BettingShop.DataLayer.DB
{
    public interface IDeserializer
    {
        public BetEvent[] DeserializeAllEvents(SQLiteDataReader reader);
        public BetEvent DeserializeOneEvent(SQLiteDataReader reader);
        public Bet[] DeserializeAllBets(SQLiteDataReader reader);
        public Bet DeserializeOneBet(SQLiteDataReader reader);
        public User[] DeserializeAllUsers(SQLiteDataReader reader);
        public User DeserializeOneUser(SQLiteDataReader reader);
    }
}