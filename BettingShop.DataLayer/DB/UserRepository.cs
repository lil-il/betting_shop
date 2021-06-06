using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BettingShop.DataLayer.Models;

namespace BettingShop.DataLayer.DB
{
    public class UserRepository : IUserRepository
    {
        private const string databaseFile = @"events.db";
        private const string connectionString = "Data source=" + databaseFile + ";";
        private SQLiteConnection connection;
        private IDeserializer deserializer;

        public UserRepository(IDeserializer deserializer)
        {
            if (!File.Exists(databaseFile))
            {
                SQLiteConnection.CreateFile(databaseFile);
            }
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            CreateTable();
            this.deserializer = deserializer;
        }

        private void CreateTable()
        {
            var command = CommandBuilder.CreateUserTable(connection);
            command.ExecuteNonQuery();
        }

        private void DeleteTable()
        {
            var command = CommandBuilder.DeleteTable(connection, "users");
            command.ExecuteNonQuery();
        }

        public async Task<List<User>> GetAllAsync()
        {
            var events = new List<BetEvent>();
            var command = CommandBuilder.BuildGetAllCommand("users", connection);
            var reader = command.ExecuteReader();
            return deserializer.DeserializeAllUsers(reader);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var command = CommandBuilder.BuildGetByIdCommand(id, connection, "users");
            var reader = command.ExecuteReader();
            return deserializer.DeserializeOneUser(reader);
        }

        public async Task<User> CreateAsync(User user)
        {
            var command = CommandBuilder.BuildCreateUserCommand(user, connection);
            var reader = command.ExecuteReader();
            return await GetByIdAsync(user.Id);
        }

        public async Task<User> DeleteAsync(Guid id)
        {
            var commandToGetId = CommandBuilder.BuildGetByIdCommand(id, connection, "users");
            var returnReader = commandToGetId.ExecuteReader();
            var command = CommandBuilder.BuildDeleteCommand(id, connection, "users");
            command.ExecuteReader();
            return deserializer.DeserializeOneUser(returnReader);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var command = CommandBuilder.BuildUpdateUserCommand(user, connection);
            command.ExecuteReader();
            return await GetByIdAsync(user.Id);
        }
    }
}
