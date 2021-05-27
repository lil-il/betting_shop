using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingShop.DataBase.DB
{
    public class Deserializer
    {
        public static BetEvent.Api.Models.BetEvent[] DeserializeAll(SQLiteDataReader reader)
        {
            var events = new List<BetEvent.Api.Models.BetEvent>();
            while (reader.Read())
            {
                events.Add(new BetEvent.Api.Models.BetEvent
                {
                    Id = Guid.Parse(reader["id"].ToString()),
                    Name = reader["name"].ToString(),
                    Description = reader["description"].ToString(),
                    BetDeadline = DateTime.Parse(reader["betdeadline"].ToString())
                });
            }
            return events.ToArray();
        }

        public static BetEvent.Api.Models.BetEvent DeserializeOne(SQLiteDataReader reader)
        {
            return DeserializeAll(reader)[0];
        }
    }
}
