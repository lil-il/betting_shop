using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BettingShop.DataLayer.Models;

namespace BettingShop.DataLayer.DB
{
    public class Deserializer : IDeserializer
    {
        public BetEvent[] DeserializeAll(SQLiteDataReader reader)
        {
            var events = new List<BetEvent>();
            while (reader.Read())
            {
                events.Add(new BetEvent
                {
                    Id = Guid.Parse(reader["id"].ToString()),
                    Name = reader["name"].ToString(),
                    Description = reader["description"].ToString(),
                    BetDeadline = DateTime.Parse(reader["betdeadline"].ToString())
                });
            }
            return events.ToArray();
        }

        public BetEvent DeserializeOne(SQLiteDataReader reader)
        {
            return DeserializeAll(reader)[0];
        }
    }
}
