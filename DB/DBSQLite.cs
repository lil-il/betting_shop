using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DB
{
    public class DBSQLite
    {
        private const string databaseName =
           @"C:\Users\andrl\source\repos\WebApplication1\WebApplication1\Tables\events.db;";

        public DBSQLite()
        {
            if (!File.Exists(databaseName))
            {
                SQLiteConnection.CreateFile(databaseName);
            }
        }

        public void CreateEventTable(string name)
        {
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=C:\\Users\\andrl\\source\\repos\\WebApplication1\\WebApplication1\\Tables\\events.db;"))
            {
                string commandText =
                    "CREATE TABLE IF NOT EXISTS [events] ( [id] INTEGER, [name] NVARCHAR(128), [description] NVARCHAR(1024))";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
            }
        }

        public void DeleteTable(string name)
        {
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=C:\\Users\\andrl\\source\\repos\\WebApplication1\\WebApplication1\\Tables\\events.db;"))
            {
                string commandText = "DROP Table events";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
            }
        }

        public void CreateEvent(ExistingEvent Event)
        {
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=C:\\Users\\andrl\\source\\repos\\WebApplication1\\WebApplication1\\Tables\\events.db;"))
            {
                string commandText = @"INSERT INTO events VALUES('5', 'name of event', 'description_of_event')";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
            }
        }

        public void UpdateEvent(ExistingEvent Event)
        {
            using (SQLiteConnection Connect = new SQLiteConnection("Data Source=C:\\Users\\andrl\\source\\repos\\WebApplication1\\WebApplication1\\Tables\\events.db;"))
            {
                string commandText = @"UPDATE events SET id = 7 WHERE id = 5";
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();
                Connect.Close();
            }
        }

        public void DeleteEvent(int Id)
        {
            throw new NotImplementedException();
        }

        public List<ExistingEvent> GetAll()
        {
            List<ExistingEvent> events = new List<ExistingEvent>();
            string connect = "SELECT * FROM events";

            using (var connection = new SQLiteConnection("Data Source=C:\\Users\\andrl\\source\\repos\\WebApplication1\\WebApplication1\\Tables\\events.db;"))
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

                            ExistingEvent existingEvent = new ExistingEvent(id, name, description);
                            events.Add(existingEvent);
                        }
                    }
                }
            }
            return events;
        }
    }
}