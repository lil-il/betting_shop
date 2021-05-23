using System;
using System.Threading.Tasks;
using BetEvent.Api.Client.Models;
using NUnit.Framework;

namespace BetEvent.Api.Client.Tests
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public async Task TestMethod()
        {
            var client = new BetEventClient("http://localhost:27254");
            var createdEvent = await client.CreateAsync(new BetEventMeta {Name = "Alex", BetDeadline = DateTime.Now});
            var allEvents = await client.GetAllAsync();
            var anotherCreatedEvent = await client.CreateAsync(new BetEventMeta { Name = "Steven", BetDeadline = DateTime.Now});
            var updatedEvent = await client.UpdateAsync(new Models.BetEvent
                {Id = createdEvent.Id, Name = "Sam", Description = "Apple", BetDeadline = DateTime.Now});
            var newEvent = await client.GetAsync(updatedEvent.Id);
            var deletedEvent = await client.DeleteAsync(newEvent.Id);
            var allEventsAfterDeletion = await client.GetAllAsync();
            System.Console.WriteLine(allEventsAfterDeletion); 
        }
    }
}
