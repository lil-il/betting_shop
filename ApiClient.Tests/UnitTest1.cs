using System;
using ApiClient.Models;
using NUnit.Framework;

namespace ApiClient.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var client = new BetEventClient("http://localhost:27254");
            var createdEvent = client.Create(new BetEventMeta {Name = "Alex", BetDeadline = DateTime.Now}).Result;
            var allEvents = client.GetAll().Result;
            var anotherCreatedEvent = client.Create(new BetEventMeta { Name = "Steven", BetDeadline = DateTime.Now }).Result;
            var updatedEvent = client.Update(new BetEvent
                {Id = 1, Name = "Sam", Description = "Apple", BetDeadline = DateTime.Now}).Result;
            var newEvent = client.Get(1).Result;
            var deletedEvent = client.Delete(1).Result;
            var allEventsAfterDeletion = client.GetAll().Result;
            return;
        }
    }
}
