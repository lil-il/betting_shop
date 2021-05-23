using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BetEvent.Api.Client.Models;
using Newtonsoft.Json;

namespace BetEvent.Api.Client
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
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Bet>(await httpResponse.Content.ReadAsStringAsync());
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
    }
}
