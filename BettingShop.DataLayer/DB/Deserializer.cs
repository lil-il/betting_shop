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
        public BetEvent[] DeserializeAllEvents(SQLiteDataReader reader)
        {
            var events = new List<BetEvent>();
            while (reader.Read())
            {
                events.Add(new BetEvent
                {
                    Id = Guid.Parse(reader["id"].ToString()),
                    Name = reader["name"].ToString(),
                    Description = reader["description"].ToString(),
                    BetDeadline = DateTime.Parse(reader["betdeadline"].ToString()),
                    Outcomes = reader["outcomes"].ToString()
                });
            }
            return events.ToArray();
        }

        public BetEvent DeserializeOneEvent(SQLiteDataReader reader)
        {
            return DeserializeAllEvents(reader)[0];
        }

        public Bet[] DeserializeAllBets(SQLiteDataReader reader)
        {
            var bets = new List<Bet>();
            while (reader.Read())
            {
                bets.Add(new Bet
                {
                    Id = Guid.Parse(reader["id"].ToString()),
                    BetSize = reader.GetInt32(1),
                    EventId = Guid.Parse(reader["event-id"].ToString()),
                    UserId = Guid.Parse(reader["user-id"].ToString()),
                    Outcome = reader["outcome"].ToString()
                });
            }
            return bets.ToArray();
        }

        public Bet DeserializeOneBet(SQLiteDataReader reader)
        {
            var res = DeserializeAllBets(reader)[0];
            return res;
        }

        public User[] DeserializeAllUsers(SQLiteDataReader reader)
        {
            var users = new List<User>();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = Guid.Parse(reader["id"].ToString()),
                    Balance = reader.GetInt32(1),
                    ParticipateBetsId = reader["participate-bets-id"].ToString(),
                    TelegramId = reader.GetInt64(3)
                });
            }
            return users.ToArray();
        }

        public User DeserializeOneUser(SQLiteDataReader reader)
        {
            return DeserializeAllUsers(reader)[0];
        }
    }
}
