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

        public BetEventClient(string address)
        {
            _address = address;
        }

        public BetEvent Create(BetEventMeta betEventMeta)
        {
            var betEventJson = JsonConvert.SerializeObject(betEventMeta);
            var httpClient = new HttpClient();
            var stringContent = new StringContent(betEventJson, Encoding.UTF8);
            var result = httpClient.PostAsync($"{_address}/api/BetEvent", stringContent).Result;
            result.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<BetEvent>(result.Content.ReadAsStringAsync().Result);
        }

        public BetEvent Delete(int id)
        {
            throw new NotImplementedException();
        }

        public BetEvent Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BetEvent> GetAll()
        {
            throw new NotImplementedException();
        }

        public BetEvent Update(BetEvent betEvent)
        {
            throw new NotImplementedException();
        }
    }
}
