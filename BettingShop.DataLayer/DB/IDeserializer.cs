using System.Collections.Generic;
using System.Data.SQLite;
using BettingShop.DataLayer.Models;

namespace BettingShop.DataLayer.DB
{
    public interface IDeserializer
    {
        public List<BetEvent> DeserializeAllEvents(SQLiteDataReader reader);
        public BetEvent DeserializeOneEvent(SQLiteDataReader reader);
        public List<Bet> DeserializeAllBets(SQLiteDataReader reader);
        public Bet DeserializeOneBet(SQLiteDataReader reader);
        public List<User> DeserializeAllUsers(SQLiteDataReader reader);
        public User DeserializeOneUser(SQLiteDataReader reader);
    }
}