using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using ExistingEventApi.Models;

namespace WebApplicationDataBase
{
    public class DBSQLite
    {
        private const string databaseName = "events.db";

        public DBSQLite()
        {
            if (!File.Exists(databaseName))
            {
                SQLiteConnection.CreateFile(databaseName);
                CreateEventTable();
            }
        }

        public void CreateEventTable()
        {
            using (SQLiteConnection Connect = new SQLiteConnection(databaseName))
            {
                string commandText =
                    "CREATE TABLE IF NOT EXISTS [events] ( [id] INTEGER, [name] NVARCHAR(150), [description] NVARCHAR(1024), [betdeadline] INTEGER)";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
            }
        }

        public void DeleteTable(string name)
        {
            using (SQLiteConnection Connect = new SQLiteConnection(databaseName))
            {
                string commandText = "DROP Table events";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
            }
        }

        public void Create(ExistingEvent Event)
        {
            using (SQLiteConnection Connect = new SQLiteConnection(databaseName))
            {
                string commandText = string.Format("INSERT INTO events VALUES({}, {})", Event.Id, Event.Name);
                // string commandText = string.Format("INSERT INTO events VALUES({}, {}, {})", Event.Id, Event.Name, "Event.Description", Event.BetDeadline);
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
            }
        }

        public void Update(ExistingEvent Event)
        {
            using (SQLiteConnection Connect = new SQLiteConnection(databaseName))
            {
                string commandText = string.Format(
                    "UPDATE events SET name = {}, description = '', betdeadline = '' WHERE id = {}",
                    Event.Name, Event.Id);
                /* string commandText =
                     string.Format("UPDATE events SET name = {}, description = {}, betdeadline = {} WHERE id = {}",
                         Event.Name, Event.Description, Event.BetDeadline, Event.Id); */
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
            }
        }

        public void Delete(int Id)
        {
            string commandText = string.Format("DELETE from events WHERE id = {}", Id);
        }

        public List<ExistingEvent> GetAll()
        {
            List<ExistingEvent> events = new List<ExistingEvent>();
            string connect = "SELECT * FROM events";

            using (var connection = new SQLiteConnection(databaseName))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(connect, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            DateTime betDeadline = DateTime.Parse(reader.GetString(3));

                            ExistingEvent existingEvent = new ExistingEvent(name);
                            /* existingEvent.Id = id;
                             existingEvent.Name = name;
                             existingEvent.Description = description;
                             existingEvent.BetDeadline = betDeadline;
                             events.Add(existingEvent); */
                        }
                    }
                }
            }
            return events;
        }
    }
}