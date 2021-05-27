using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using BettingShop.DataBase.DB;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BettingShop.DataBase.Tests
{
    [TestClass]
    public class EventRepositoryTests
    {
        [TestMethod]
        public void CreateTableTest()
        {
            var repo = new EventRepository();
        }

        [TestMethod]
        public async Task CreateTest()
        {
            var repo = new EventRepository();
            var event1 = new BetEvent.Api.Models.BetEvent
            { Id = Guid.NewGuid(), Name = "name1", Description = "desc1", BetDeadline = DateTime.Now };
            var createdEvent = await repo.Create(event1);
            Assert.AreEqual(event1.Id, createdEvent.Id);
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            var repo = new EventRepository();
            var event1 = new BetEvent.Api.Models.BetEvent
            { Id = Guid.NewGuid(), Name = "name1", Description = "desc1", BetDeadline = DateTime.Now };
            await repo.Create(event1);
            var event2 = new BetEvent.Api.Models.BetEvent
            { Id = event1.Id, Name = "namenew", Description = "descnew", BetDeadline = DateTime.Now };
            var updatedEvent = await repo.Update(event2);
            Assert.AreEqual(updatedEvent.Id, event1.Id);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            var repo = new EventRepository();
            var event1 = new BetEvent.Api.Models.BetEvent
            { Id = Guid.NewGuid(), Name = "name1", Description = "desc1", BetDeadline = DateTime.Now };
            await repo.Create(event1);
            await repo.Delete(event1.Id);
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            var repo = new EventRepository();
            var lengthBefore = (await repo.GetExistingEvents()).Length;
            var event1 = new BetEvent.Api.Models.BetEvent
            { Id = Guid.NewGuid(), Name = "name1", Description = "desc1", BetDeadline = DateTime.Now };
            await repo.Create(event1);
            Thread.Sleep(1000);
            var event2 = new BetEvent.Api.Models.BetEvent
            { Id = Guid.NewGuid(), Name = "name2", Description = "desc2", BetDeadline = DateTime.Now };
            await repo.Create(event2);
            Thread.Sleep(1000);
            var event3 = new BetEvent.Api.Models.BetEvent
            { Id = Guid.NewGuid(), Name = "name3", Description = "desc3", BetDeadline = DateTime.Now };
            await repo.Create(event3);
            var events = await repo.GetExistingEvents();
            Assert.AreEqual(lengthBefore + 3, events.Length);
        }
    }
}