using ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public BetEvent Create(BetEventMeta betEventMeta)
        {
            var betEventJson = JsonConvert.SerializeObject(betEventMeta);
            var stringContent = new StringContent(betEventJson, Encoding.UTF8, "application/json");
            var httpResponse = httpClient.PostAsync($"{_address}/api/BetEvent", stringContent).Result;
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<BetEvent>(httpResponse.Content.ReadAsStringAsync().Result);
        }

        public BetEvent Delete(int id)
        {
            var deletedEvent = Get(id);
            var httpResponse = httpClient.DeleteAsync($"{_address}/api/BetEvent/{id}");
            return deletedEvent;
        }

        public BetEvent Get(int id)
        {
            var httpResponse = httpClient.GetAsync($"{_address}/api/BetEvent/{id}").Result;
            return JsonConvert.DeserializeObject<BetEvent>(httpResponse.Content.ReadAsStringAsync().Result);
        }

        public IEnumerable<BetEvent> GetAll()
        {
            var httpResponse = httpClient.GetAsync($"{_address}/api/BetEvent").Result;
            return JsonConvert.DeserializeObject<IEnumerable<BetEvent>>(httpResponse.Content.ReadAsStringAsync().Result);
        }

        public BetEvent Update(BetEvent betEvent)
        {
            var betEventJson = JsonConvert.SerializeObject(betEvent);
            var stringContent = new StringContent(betEventJson, Encoding.UTF8, "application/json");
            var httpResponse = httpClient.PutAsync($"{_address}/api/BetEvent/{betEvent.Id}", stringContent).Result;
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<BetEvent>(httpResponse.Content.ReadAsStringAsync().Result);
        }
    }
}
