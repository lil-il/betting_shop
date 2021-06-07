using System;
using System.Threading.Tasks;
using BettingShop.Api.Client.Models;
using NUnit.Framework;

namespace BettingShop.Api.Client.Tests
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public async Task CRUD_BetEvent_Test()
        {
            var client = new BetEventClient("http://localhost:27254");
            var createdEvent = await client.CreateAsync(new BetEventMeta { Name = "Alex", BetDeadline = DateTime.Now, Outcomes = "kdsmd" });
            var allEvents = await client.GetAllAsync();
            var anotherCreatedEvent = await client.CreateAsync(new BetEventMeta { Name = "Steven", BetDeadline = DateTime.Now, Outcomes = "ksmck" });
            var updatedEvent = await client.UpdateAsync(new Models.BetEvent
            { Id = createdEvent.Id, Name = "Sam", Description = "Apple", BetDeadline = DateTime.Now, Outcomes = "ksdn" });
            var newEvent = await client.GetAsync(updatedEvent.Id);
            var deletedEvent = await client.DeleteAsync(newEvent.Id);
            var allEventsAfterDeletion = await client.GetAllAsync();
            Console.WriteLine(allEventsAfterDeletion);
        }

        [Test]
        public async Task CRUD_Bet_Test()
        {
            var client = new BetClient("http://localhost:27254");
            var createdBet = await client.CreateAsync(new BetMeta { BetSize = 19, EventId = Guid.NewGuid(), UserId = Guid.NewGuid(), Outcome = "win" });
            var allBets = await client.GetAllAsync();
            var anotherCreatedBet = await client.CreateAsync(new BetMeta { BetSize = 4, EventId = Guid.NewGuid(), UserId = Guid.NewGuid(), Outcome = "lose" });
            var updatedBet = await client.UpdateAsync(new Bet
            { Id = createdBet.Id, BetSize = 100, EventId = createdBet.EventId, UserId = createdBet.UserId, Outcome = "lose" });
            var newBet = await client.GetAsync(updatedBet.Id);
            var deletedBet = await client.DeleteAsync(newBet.Id);
            var allBetsAfterDeletion = await client.GetAllAsync();
            Console.WriteLine(allBetsAfterDeletion);
        }

        [Test]
        public async Task CRUD_User_Test()
        {
            var client = new UserClient("http://localhost:27254");
            var createdUser = await client.CreateAsync(new UserMeta { Balance = 1000, TelegramId = 1 });
            var allUsers = await client.GetAllAsync();
            var anotherCreatedUser = await client.CreateAsync(new UserMeta { Balance = 0,  TelegramId = 2 });
            var updatedUser = await client.UpdateAsync(new User
            { Id = createdUser.Id, Balance = 900, TelegramId = 1 });
            var unknownUser = await client.GetByTelegramIdAsync(2);
            var newUser = await client.GetAsync(updatedUser.Id);
            var userBiTelegramId = await client.GetByTelegramIdAsync(2);
            var deletedUser = await client.DeleteAsync(newUser.Id);
            var allUsersAfterDeletion = await client.GetAllAsync();
            Console.WriteLine(allUsersAfterDeletion);
        }
    }
}