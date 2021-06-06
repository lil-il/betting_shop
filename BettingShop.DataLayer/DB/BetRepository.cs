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
    public class BetRepository : IBetRepository
    {
        private const string databaseFile = @"events.db";
        private const string connectionString = "Data source=" + databaseFile + ";";
        private SQLiteConnection connection;
        private IDeserializer deserializer;

        public BetRepository(IDeserializer deserializer)
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
            var command = CommandBuilder.CreateBetTable(connection);
            command.ExecuteNonQuery();
        }

        private void DeleteTable()
        {
            var command = CommandBuilder.DeleteTable(connection, "bets");
            command.ExecuteNonQuery();
        }

        public async Task<List<Bet>> GetAllAsync()
        {
            var events = new List<BetEvent>();
            var command = CommandBuilder.BuildGetAllCommand("bets", connection);
            var reader = command.ExecuteReader();
            return deserializer.DeserializeAllBets(reader);
        }

        public async Task<Bet> GetByIdAsync(Guid id)
        {
            var command = CommandBuilder.BuildGetByIdCommand(id, connection, "bets");
            var reader = command.ExecuteReader();
            return deserializer.DeserializeOneBet(reader);
        }

        public async Task<Bet> CreateAsync(Bet bet)
        {
            var command = CommandBuilder.BuildCreateBetCommand(bet, connection);
            var reader = command.ExecuteReader();
            var res = await GetByIdAsync(bet.Id);
            var a = 2;
            return res;
        }

        public async Task<Bet> DeleteAsync(Guid id)
        {
            var commandToGetId = CommandBuilder.BuildGetByIdCommand(id, connection, "bets");
            var returnReader = commandToGetId.ExecuteReader();
            var command = CommandBuilder.BuildDeleteCommand(id, connection, "bets");
            command.ExecuteReader();
            return deserializer.DeserializeOneBet(returnReader);
        }

        public async Task<Bet> UpdateAsync(Bet bet)
        {
            var command = CommandBuilder.BuildUpdateBetCommand(bet, connection);
            command.ExecuteReader();
            return await GetByIdAsync(bet.Id);
        }
    }
}
