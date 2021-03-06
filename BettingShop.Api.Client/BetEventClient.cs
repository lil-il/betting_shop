using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BettingShop.Api.Client.Models;
using Newtonsoft.Json;

namespace BettingShop.Api.Client
{
    public class BetEventClient : IBetEventClient
    {
        private readonly string _address;
        private HttpClient httpClient = new HttpClient();

        public BetEventClient(string address)
        {
            _address = address;
        }

        public async Task<Models.BetEvent> CreateAsync(BetEventMeta betEventMeta)
        {
            var betEventJson = JsonConvert.SerializeObject(betEventMeta);
            var stringContent = new StringContent(betEventJson, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync($"{_address}/api/BetEvent", stringContent);
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Models.BetEvent>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<Models.BetEvent> DeleteAsync(Guid id)
        {
            var httpResponse = await httpClient.DeleteAsync($"{_address}/api/BetEvent/{id}");
            return JsonConvert.DeserializeObject<Models.BetEvent>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<Models.BetEvent> GetAsync(Guid id)
        {
            var httpResponse = await httpClient.GetAsync($"{_address}/api/BetEvent/{id}");
            return JsonConvert.DeserializeObject<Models.BetEvent>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<Models.BetEvent[]> GetAllAsync()
        {
            var httpResponse = await httpClient.GetAsync($"{_address}/api/BetEvent");
            return JsonConvert.DeserializeObject<Models.BetEvent[]>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<Models.BetEvent> UpdateAsync(Models.BetEvent betEvent)
        {
            var betEventJson = JsonConvert.SerializeObject(betEvent);
            var stringContent = new StringContent(betEventJson, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PutAsync($"{_address}/api/BetEvent/{betEvent.Id.ToString()}", stringContent);
            httpResponse.EnsureSuccessStatusCode();
            return betEvent;
        }

        public async Task<BetEvent> CloseEventAsync(Guid id, string winOutcome)
        {
            var betClient = new BetClient(_address);
            var userClient = new UserClient(_address);
            var sumOfMoney = await betClient.SumOfMoneyForEvent(id);
            var winners = new List<Bet>(await betClient.AllWinBetsForBetEventAsync(id, winOutcome)).Select(t => t.UserId).ToHashSet();
            var allBets = await betClient.AllBetsForBetEventAsync(id);
            if (winners.Count != 0)
            {
                var gain = sumOfMoney / winners.Count;
                foreach (var winner in winners)
                {
                    var winnerUser = await userClient.GetAsync(winner);
                    winnerUser.Balance += gain;
                    await userClient.UpdateAsync(winnerUser);
                }
            }
            foreach (var bet in allBets)
                await betClient.DeleteAsync(bet.Id);
            var eventForClosing = await DeleteAsync(id);
            return eventForClosing;
        }

        public async Task<BetEvent[]> GetAllEventsFromCreatorAsync(long creatorId)
        {
            var events = await GetAllAsync();
            return new List<BetEvent>(events).FindAll(t => t.CreatorId == creatorId).ToArray();
        }
    }
}
