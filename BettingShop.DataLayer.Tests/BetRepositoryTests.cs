using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BettingShop.DataLayer.DB;
using BettingShop.DataLayer.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BettingShop.DataLayer.Tests
{
    [TestClass]
    public class BetRepositoryTests
    {
        [TestMethod]
        public void CreateTableTest()
        {
            var repo = new BetRepository(new Deserializer());
        }

        [TestMethod]
        public async Task CreateTest()
        {
            var repo = new BetRepository(new Deserializer());
            var bet1 = new Bet
            { Id = Guid.NewGuid(), BetSize = 4, UserId = Guid.NewGuid(), EventId = Guid.NewGuid(), Outcome = "outcome" };
            var createdEvent = await repo.CreateAsync(bet1);
            Assert.AreEqual(bet1.Id, createdEvent.Id);
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            var repo = new BetRepository(new Deserializer());
            var bet1 = new Bet
            { Id = Guid.NewGuid(), BetSize = 5, UserId = Guid.NewGuid(), EventId = Guid.NewGuid(), Outcome = "outcome2" };
            await repo.CreateAsync(bet1);
            var bet2 = new Bet
            { Id = bet1.Id, BetSize = 7, UserId = bet1.UserId, EventId = bet1.EventId, Outcome = "outcome2" };
            var updatedEvent = await repo.UpdateAsync(bet2);
            Assert.AreEqual(updatedEvent.Id, bet2.Id);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            var repo = new BetRepository(new Deserializer());
            var bet3 = new Bet
            { Id = Guid.NewGuid(), BetSize = 7, UserId = Guid.NewGuid(), EventId = Guid.NewGuid(), Outcome = "outcome3" };
            await repo.CreateAsync(bet3);
            await repo.DeleteAsync(bet3.Id);
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            var repo = new BetRepository(new Deserializer());
            var lengthBefore = (await repo.GetAllAsync()).Count;
            var bet1 = new Bet
            { Id = Guid.NewGuid(), BetSize = 7, UserId = Guid.NewGuid(), EventId = Guid.NewGuid(), Outcome = "outcome3" };
            await repo.CreateAsync(bet1);
            Thread.Sleep(1000);
            var bet2 = new Bet
            { Id = Guid.NewGuid(), BetSize = 9, UserId = Guid.NewGuid(), EventId = Guid.NewGuid(), Outcome = "outcome5" };
            await repo.CreateAsync(bet2);
            Thread.Sleep(1000);
            var bet3 = new Bet
            { Id = Guid.NewGuid(), BetSize = 12, UserId = Guid.NewGuid(), EventId = Guid.NewGuid(), Outcome = "outcome3" };
            await repo.CreateAsync(bet3);
            var events = await repo.GetAllAsync();
            Assert.AreEqual(lengthBefore + 3, events.Count);
        }
    }
}
