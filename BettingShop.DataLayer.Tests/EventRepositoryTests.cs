using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using BettingShop.DataLayer.DB;
using BettingShop.DataLayer.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BettingShop.DataLayer.Tests
{
    [TestClass]
    public class EventRepositoryTests
    {
        [TestMethod]
        public void CreateTableTest()
        {
            var repo = new EventRepository(new Deserializer());
        }

        [TestMethod]
        public async Task CreateTest()
        {
            var repo = new EventRepository(new Deserializer());
            var event1 = new BetEvent
            { Id = Guid.NewGuid(), Name = "name1", Description = "desc1", BetDeadline = DateTime.Now };
            var createdEvent = await repo.CreateAsync(event1);
            Assert.AreEqual(event1.Id, createdEvent.Id);
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            var repo = new EventRepository(new Deserializer());
            var event1 = new BetEvent
            { Id = Guid.NewGuid(), Name = "name1", Description = "desc1", BetDeadline = DateTime.Now };
            await repo.CreateAsync(event1);
            var event2 = new BetEvent
            { Id = event1.Id, Name = "namenew", Description = "descnew", BetDeadline = DateTime.Now };
            var updatedEvent = await repo.UpdateAsync(event2);
            Assert.AreEqual(updatedEvent.Id, event1.Id);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            var repo = new EventRepository(new Deserializer());
            var event1 = new BetEvent
            { Id = Guid.NewGuid(), Name = "name1", Description = "desc1", BetDeadline = DateTime.Now };
            await repo.CreateAsync(event1);
            await repo.DeleteAsync(event1.Id);
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            var repo = new EventRepository(new Deserializer());
            var lengthBefore = (await repo.GetAllAsync()).Count;
            var event1 = new BetEvent
            { Id = Guid.NewGuid(), Name = "name1", Description = "desc1", BetDeadline = DateTime.Now };
            await repo.CreateAsync(event1);
            Thread.Sleep(1000);
            var event2 = new BetEvent
            { Id = Guid.NewGuid(), Name = "name2", Description = "desc2", BetDeadline = DateTime.Now };
            await repo.CreateAsync(event2);
            Thread.Sleep(1000);
            var event3 = new BetEvent
            { Id = Guid.NewGuid(), Name = "name3", Description = "desc3", BetDeadline = DateTime.Now };
            await repo.CreateAsync(event3);
            var events = await repo.GetAllAsync();
            Assert.AreEqual(lengthBefore + 3, events.Count);
        }
    }
}