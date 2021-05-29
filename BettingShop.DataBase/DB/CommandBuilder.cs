using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetEvent.Api.Models;

namespace BettingShop.DataBase.DB
{
    static class CommandBuilder
    {
        public static SQLiteCommand BuildGetAllCommand(string table, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("SELECT * from {0}", table), connection);
        }

        public static SQLiteCommand CreateEventTable(SQLiteConnection connection)
        {
            return new SQLiteCommand(
                "CREATE TABLE IF NOT EXISTS [events] ([id] NVARCHAR(32), [name] NVARCHAR(150), [description] NVARCHAR(1024), [betdeadline] NVARCHAR(100))", connection);
        }

        public static SQLiteCommand BuildGetByIdCommand(Guid id, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("SELECT * FROM events WHERE id = '{0}'", id.ToString()), connection);
        }

        public static SQLiteCommand BuildCreateCommand(BetEvent.Api.Models.BetEvent betEvent, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("INSERT INTO events VALUES('{0}', '{1}', '{2}', '{3}')", betEvent.Id.ToString(),
                betEvent.Name, betEvent.Description, betEvent.BetDeadline.ToString()), connection);
        }

        public static SQLiteCommand BuildDeleteCommand(Guid id, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("DELETE FROM events WHERE id = '{0}'", id.ToString()), connection);
        }

        public static SQLiteCommand BuildUpdateCommand(BetEvent.Api.Models.BetEvent betEvent, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("UPDATE events SET name = '{0}', description = '{1}', betdeadline = '{2}' WHERE id = '{3}'",
                betEvent.Name, betEvent.Description, betEvent.BetDeadline.ToString(), betEvent.Id.ToString()), connection);
        }

        public static SQLiteCommand DeleteEventTable(SQLiteConnection connection)
        {
            return new SQLiteCommand("DROP TABLE events", connection);
        }
    }
}
