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
    public class UserRepositoryTests
    {
        [TestMethod]
        public void CreateTableTest()
        {
            var repo = new UserRepository(new Deserializer());
        }

        [TestMethod]
        public async Task CreateTest()
        {
            var repo = new UserRepository(new Deserializer());
            var user1 = new User
            { Id = Guid.NewGuid(), Balance = 1000, TelegramId = 15321, ParticipateBetsId = "outcome1 : 213" };
            var createdUser = await repo.CreateAsync(user1);
            Assert.AreEqual(user1.Id, createdUser.Id);
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            var repo = new UserRepository(new Deserializer());
            var user1 = new User
            { Id = Guid.NewGuid(), Balance = 1000, TelegramId = 15321, ParticipateBetsId = "outcome1 : 213" };
            await repo.CreateAsync(user1);
            var user2 = new User
            { Id = user1.Id, Balance = 850, TelegramId = 15321, ParticipateBetsId = "outcome1 : 213, outcome2 : 150" };
            var updatedUser = await repo.UpdateAsync(user2);
            Assert.AreEqual(updatedUser.Id, user2.Id);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            var repo = new UserRepository(new Deserializer());
            var user1 = new User
            { Id = Guid.NewGuid(), Balance = 1000, TelegramId = 15321, ParticipateBetsId = "outcome1 : 213" };
            await repo.CreateAsync(user1);
            await repo.DeleteAsync(user1.Id);
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            var repo = new UserRepository(new Deserializer());
            var lengthBefore = (await repo.GetAllAsync()).Length;
            var user1 = new User
            { Id = Guid.NewGuid(), Balance = 1000, TelegramId = 15321, ParticipateBetsId = "outcome1 : 213" };
            await repo.CreateAsync(user1);
            Thread.Sleep(1000);
            var user2 = new User
            { Id = Guid.NewGuid(), Balance = 1500, TelegramId = 15751, ParticipateBetsId = "outcome1 : 253" };
            await repo.CreateAsync(user2);
            Thread.Sleep(1000);
            var user3 = new User
            { Id = Guid.NewGuid(), Balance = 1500, TelegramId = 15754, ParticipateBetsId = "outcome1 : 285" };
            await repo.CreateAsync(user3);
            var events = await repo.GetAllAsync();
            Assert.AreEqual(lengthBefore + 3, events.Length);
        }
    }
}
