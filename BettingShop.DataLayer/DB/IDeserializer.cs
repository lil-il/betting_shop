using System.Data.SQLite;
using BettingShop.DataLayer.Models;

namespace BettingShop.DataLayer.DB
{
    public interface IDeserializer
    {
        public BetEvent[] DeserializeAll(SQLiteDataReader reader);

        public BetEvent DeserializeOne(SQLiteDataReader reader);
    }
}