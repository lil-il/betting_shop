using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BettingShop.DataLayer.Models;

namespace BettingShop.DataLayer.DB
{
    public class EventRepository : IEventRepository
    {
        private const string databaseFile = @"events.db";
        private const string connectionString = "Data source=" + databaseFile + ";";
        private SQLiteConnection connection;
        private IDeserializer deserializer;

        public EventRepository(IDeserializer deserializer)
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
            var command = CommandBuilder.CreateEventTable(connection);
            command.ExecuteNonQuery();
        }

        private void DeleteTable()
        {
            var command = CommandBuilder.DeleteTable(connection, "events");
            command.ExecuteNonQuery();
        }

        public async Task<List<BetEvent>> GetAllAsync()
        {
            var events = new List<BetEvent>();
            var command = CommandBuilder.BuildGetAllCommand("events", connection);
            var reader = command.ExecuteReader();
            return deserializer.DeserializeAllEvents(reader);
        }

        public async Task<BetEvent> GetByIdAsync(Guid id)
        {
            var command = CommandBuilder.BuildGetByIdCommand(id, connection, "events");
            var reader = command.ExecuteReader();
            return deserializer.DeserializeOneEvent(reader);
        }

        public async Task<BetEvent> CreateAsync(BetEvent betEvent)
        {
            var command = CommandBuilder.BuildCreateEventCommand(betEvent, connection);
            var reader = command.ExecuteReader();
            return await GetByIdAsync(betEvent.Id);
        }

        public async Task<BetEvent> DeleteAsync(Guid id)
        {
            var commandToGetId = CommandBuilder.BuildGetByIdCommand(id, connection, "events");
            var returnReader = commandToGetId.ExecuteReader();
            var command = CommandBuilder.BuildDeleteCommand(id, connection, "events");
            command.ExecuteReader();
            return deserializer.DeserializeOneEvent(returnReader);
        }

        public async Task<BetEvent> UpdateAsync(BetEvent betEvent)
        {
            var command = CommandBuilder.BuildUpdateEventCommand(betEvent, connection);
            command.ExecuteReader();
            return await GetByIdAsync(betEvent.Id);
        }
    }
}
