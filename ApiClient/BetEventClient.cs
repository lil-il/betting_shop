using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BetEvent.Api.Client.Models;
using Newtonsoft.Json;

namespace BetEvent.Api.Client
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
    }
}
