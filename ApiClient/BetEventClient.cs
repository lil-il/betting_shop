using ApiClient.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiClient
{
    public class BetEventClient : IBetEventClient
    {
        private readonly string _address;
        private HttpClient httpClient = new HttpClient();

        public BetEventClient(string address)
        {
            _address = address;
        }

        public async Task<BetEvent> Create(BetEventMeta betEventMeta)
        {
            var betEventJson = JsonConvert.SerializeObject(betEventMeta);
            var stringContent = new StringContent(betEventJson, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync($"{_address}/api/BetEvent", stringContent);
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<BetEvent>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<BetEvent> Delete(int id)
        {
            var deletedEvent = Get(id);
            var httpResponse = await httpClient.DeleteAsync($"{_address}/api/BetEvent/{id}");
            return await deletedEvent;
        }

        public async Task<BetEvent> Get(int id)
        {
            var httpResponse = await httpClient.GetAsync($"{_address}/api/BetEvent/{id}");
            return JsonConvert.DeserializeObject<BetEvent>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<BetEvent[]> GetAll()
        {
            var httpResponse = await httpClient.GetAsync($"{_address}/api/BetEvent");
            return JsonConvert.DeserializeObject<BetEvent[]>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<BetEvent> Update(BetEvent betEvent)
        {
            var betEventJson = JsonConvert.SerializeObject(betEvent);
            var stringContent = new StringContent(betEventJson, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PutAsync($"{_address}/api/BetEvent/{betEvent.Id}", stringContent);
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<BetEvent>(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
