using System.Data.SQLite;

namespace BettingShop.DataBase.DB
{
    public interface IDeserializer
    {
        public BetEvent.Api.Models.BetEvent[] DeserializeAll(SQLiteDataReader reader);

        public BetEvent.Api.Models.BetEvent DeserializeOne(SQLiteDataReader reader);
    }
}