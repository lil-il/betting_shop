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
            var createdEvent = client.Create(new BetEventMeta {Name = "Alex", BetDeadline = DateTime.Now});
            var allEvents = client.GetAll();
            var anotherCreatedEvent = client.Create(new BetEventMeta { Name = "Steven", BetDeadline = DateTime.Now });
            var updatedEvent = client.Update(new BetEvent
                {Id = 1, Name = "Sam", Description = "Apple", BetDeadline = DateTime.Now});
            var newEvent = client.Get(1);
            var deletedEvent = client.Delete(1);
            var allEventsAfterDeletion = client.GetAll();
            return;
        }
    }
}
