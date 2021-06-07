using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BettingShop.Api.Client.Models;
using Newtonsoft.Json;

namespace BettingShop.Api.Client
{
    public class BetClient: IBetClient
    {
        private readonly string _address;
        private HttpClient httpClient = new HttpClient();

        public BetClient(string address)
        {
            _address = address;
        }

        public async Task<Bet> CreateAsync(BetMeta betMeta)
        {
            var betJson = JsonConvert.SerializeObject(betMeta);
            var stringContent = new StringContent(betJson, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync($"{_address}/api/Bet", stringContent);
            var newBet = JsonConvert.DeserializeObject<Bet>(await httpResponse.Content.ReadAsStringAsync());
            var userClient = new UserClient("http://localhost:27254");
            var user = await userClient.GetAsync(newBet.UserId);
            user.Balance -= newBet.BetSize;
            user = await userClient.UpdateAsync(user);
            httpResponse.EnsureSuccessStatusCode();
            return newBet;
        }

        public async Task<Bet> DeleteAsync(Guid id)
        {
            var httpResponse = await httpClient.DeleteAsync($"{_address}/api/Bet/{id}");
            return JsonConvert.DeserializeObject<Bet>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<Bet> GetAsync(Guid id)
        {
            var httpResponse = await httpClient.GetAsync($"{_address}/api/Bet/{id}");
            return JsonConvert.DeserializeObject<Bet>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<Bet[]> GetAllAsync()
        {
            var httpResponse = await httpClient.GetAsync($"{_address}/api/Bet");
            return JsonConvert.DeserializeObject<Bet[]>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<Bet> UpdateAsync(Bet bet)
        {
            var betJson = JsonConvert.SerializeObject(bet);
            var stringContent = new StringContent(betJson, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PutAsync($"{_address}/api/Bet/{bet.Id}", stringContent);
            httpResponse.EnsureSuccessStatusCode();
            return bet;
        }

        public async Task<Bet[]> AllBetsForUserAsync(int telegramId)
        {
            var userClient = new UserClient("http://localhost:27254");
            var user = await userClient.GetByTelegramIdAsync(telegramId);
            var allBets = await GetAllAsync();
            return new List<Bet>(allBets).FindAll(t => t.UserId == user.Id).ToArray();
        }
    }
}
