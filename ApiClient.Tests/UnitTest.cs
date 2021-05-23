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
        public async Task TestBetEvent()
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
            Console.WriteLine(allEventsAfterDeletion); 
        }

        [Test]
        public async Task TestBet()
        {
            var client = new BetClient("http://localhost:27254");
            var createdBet = await client.CreateAsync(new BetMeta { BetSize = 19, EventId = Guid.NewGuid(), UserId = Guid.NewGuid()});
            var allBets = await client.GetAllAsync();
            var anotherCreatedBet = await client.CreateAsync(new BetMeta { BetSize = 4, EventId = Guid.NewGuid(), UserId = Guid.NewGuid()});
            var updatedBet = await client.UpdateAsync(new Bet
                { Id = createdBet.Id, BetSize = 100, EventId = createdBet.EventId, UserId = createdBet.UserId});
            var newBet = await client.GetAsync(updatedBet.Id);
            var deletedBet = await client.DeleteAsync(newBet.Id);
            var allBetsAfterDeletion = await client.GetAllAsync();
            Console.WriteLine(allBetsAfterDeletion);
        }

        [Test]
        public async Task TestUser()
        {
            var client = new UserClient("http://localhost:27254");
            var createdUser = await client.CreateAsync(new UserMeta { Balance = 1000});
            var allUsers = await client.GetAllAsync();
            var anotherCreatedUser = await client.CreateAsync(new UserMeta { Balance = 0});
            var updatedUser = await client.UpdateAsync(new User
                { Id = createdUser.Id, Balance = 900});
            var newUser = await client.GetAsync(updatedUser.Id);
            var deletedUser = await client.DeleteAsync(newUser.Id);
            var allUsersAfterDeletion = await client.GetAllAsync();
            Console.WriteLine(allUsersAfterDeletion);
        }
    }
}
