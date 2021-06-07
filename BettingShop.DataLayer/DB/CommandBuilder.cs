using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BettingShop.DataLayer.Models;

namespace BettingShop.DataLayer.DB
{
    static class CommandBuilder
    {

        public static SQLiteCommand BuildGetAllCommand(string tablename, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("SELECT * from {0}", tablename), connection);
        }

        public static SQLiteCommand CreateEventTable(SQLiteConnection connection)
        {
            return new SQLiteCommand(
                "CREATE TABLE IF NOT EXISTS [events] ([id] NVARCHAR(32), [name] NVARCHAR(150), [description] NVARCHAR(1024), [betdeadline] NVARCHAR(100), [outcomes] NVARCHAR(1024))", connection);
        }

        public static SQLiteCommand CreateBetTable(SQLiteConnection connection)
        {
            return new SQLiteCommand(
                "CREATE TABLE IF NOT EXISTS [bets] ([id] NVARCHAR(32), [bet] INTEGER, [event_id] NVARCHAR(32), [user_id] NVARCHAR(32), [outcome] NVARCHAR(1024))",
                connection);
        }

        public static SQLiteCommand CreateUserTable(SQLiteConnection connection)
        {
            return new SQLiteCommand(
                "CREATE TABLE IF NOT EXISTS [users] ([id] NVARCHAR(32), [balance] INTEGER, [telegram_id] INTEGER)",
                connection);
        }

        public static SQLiteCommand BuildGetByIdCommand(Guid id, SQLiteConnection connection, string tablename)
        {
            return new SQLiteCommand(string.Format("SELECT * FROM {0} WHERE id = '{1}'", tablename, id.ToString()), connection);
        }

        public static SQLiteCommand BuildCreateEventCommand(BetEvent betEvent, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("INSERT INTO events VALUES('{0}', '{1}', '{2}', '{3}', '{4}')", betEvent.Id.ToString(),
                betEvent.Name, betEvent.Description, betEvent.BetDeadline.ToString(), betEvent.Outcomes), connection);
        }

        public static SQLiteCommand BuildCreateBetCommand(Bet bet, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("INSERT INTO bets VALUES('{0}', {1}, '{2}', '{3}', '{4}')", bet.Id.ToString(),
                bet.BetSize, bet.EventId.ToString(), bet.UserId.ToString(), bet.Outcome), connection);
        }

        public static SQLiteCommand BuildCreateUserCommand(User user, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("INSERT INTO users VALUES('{0}', {1}, '{2}')", user.Id.ToString(),
                user.Balance, user.TelegramId), connection);
        }

        public static SQLiteCommand BuildDeleteCommand(Guid id, SQLiteConnection connection, string tablename)
        {
            return new SQLiteCommand(string.Format("DELETE FROM {0} WHERE id = '{0}'", tablename, id.ToString()), connection);
        }


        public static SQLiteCommand BuildUpdateEventCommand(BetEvent betEvent, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("UPDATE events SET name = '{0}', description = '{1}', betdeadline = '{2}', outcomes = '{3}' WHERE id = '{4}'",
                betEvent.Name, betEvent.Description, betEvent.BetDeadline.ToString(), betEvent.Outcomes, betEvent.Id.ToString()), connection);
        }

        public static SQLiteCommand BuildUpdateBetCommand(Bet bet, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("UPDATE bets SET bet = {0}, event_id = '{1}', user_id = '{2}', outcome = '{3}' WHERE id = '{4}'",
                bet.BetSize, bet.EventId.ToString(), bet.UserId.ToString(), bet.Outcome, bet.Id.ToString()), connection);
        }

        public static SQLiteCommand BuildUpdateUserCommand(User user, SQLiteConnection connection)
        {
            return new SQLiteCommand(string.Format("UPDATE users SET balance = {0} WHERE telegram_id = {1}",
                user.Balance, user.TelegramId), connection);
        }

        public static SQLiteCommand DeleteTable(SQLiteConnection connection, string tablename)
        {
            return new SQLiteCommand(string.Format("DROP TABLE {0}", tablename), connection);
        }

    }
}
