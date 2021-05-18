using System;
using System.Threading.Tasks;
using BettingShop.Api.Client.Models;
using NUnit.Framework;

namespace BettingShop.Api.Client.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public async Task TestMethod1()
        {
            var client = new BetEventClient("http://localhost:27254");
            var createdEvent = await client.Create(new BetEventMeta {Name = "Alex", BetDeadline = DateTime.Now});
            var allEvents = await client.GetAll();
            var anotherCreatedEvent = await client.Create(new BetEventMeta { Name = "Steven", BetDeadline = DateTime.Now });
            var updatedEvent = await client.Update(new BetEvent
                {Id = createdEvent.Id, Name = "Sam", Description = "Apple", BetDeadline = DateTime.Now});
            var newEvent = await client.Get(updatedEvent.Id);
            var deletedEvent = await client.Delete(newEvent.Id);
            var allEventsAfterDeletion = await client.GetAll();
            System.Console.WriteLine(allEventsAfterDeletion);
        }
    }
}
