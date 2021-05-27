using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BetEvent.Api;

namespace BettingShop.DataBase.DB
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
            var command = CommandBuilder.DeleteEventTable(connection);
            command.ExecuteNonQuery();
        }

        public async Task<BetEvent.Api.Models.BetEvent[]> GetExistingEvents()
        {
            var events = new List<BetEvent.Api.Models.BetEvent>();
            var command = CommandBuilder.BuildGetAllCommand("events", connection);
            var reader = command.ExecuteReader();
            return deserializer.DeserializeAll(reader);
        }

        public async Task<BetEvent.Api.Models.BetEvent> GetExistingEventById(Guid id)
        {
            var command = CommandBuilder.BuildGetByIdCommand(id, connection);
            var reader = command.ExecuteReader();
            return deserializer.DeserializeOne(reader);
        }

        public async Task<BetEvent.Api.Models.BetEvent> Create(BetEvent.Api.Models.BetEvent betEvent)
        {
            var command = CommandBuilder.BuildCreateCommand(betEvent, connection);
            var reader = command.ExecuteReader();
            return await GetExistingEventById(betEvent.Id);
        }

        public async Task<BetEvent.Api.Models.BetEvent> Delete(Guid id)
        {
            var commandToGetId = CommandBuilder.BuildGetByIdCommand(id, connection);
            var returnReader = commandToGetId.ExecuteReader();
            var command = CommandBuilder.BuildDeleteCommand(id, connection);
            command.ExecuteReader();
            return deserializer.DeserializeOne(returnReader);
        }

        public async Task<BetEvent.Api.Models.BetEvent> Update(BetEvent.Api.Models.BetEvent betEvent)
        {
            var command = CommandBuilder.BuildUpdateCommand(betEvent, connection);
            command.ExecuteReader();
            return await GetExistingEventById(betEvent.Id);
        }
    }
}
