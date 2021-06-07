using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BettingShop.Api.Client.Models;
using Newtonsoft.Json;

namespace BettingShop.Api.Client
{
    public class UserClient : IUserClient
    {
        private readonly string _address;
        private HttpClient httpClient = new HttpClient();

        public UserClient(string address)
        {
            _address = address;
        }

        public async Task<User> CreateAsync(UserMeta userMeta)
        {
            var userJson = JsonConvert.SerializeObject(userMeta);
            var stringContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync($"{_address}/api/User", stringContent);
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<User>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<User> DeleteAsync(Guid id)
        {
            var httpResponse = await httpClient.DeleteAsync($"{_address}/api/User/{id}");
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<User>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<User> GetAsync(Guid id)
        {
            var httpResponse = await httpClient.GetAsync($"{_address}/api/User/{id}");
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<User>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<User[]> GetAllAsync()
        {
            var httpResponse = await httpClient.GetAsync($"{_address}/api/User");
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<User[]>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<User> UpdateAsync(User user)
        {
            var userJson = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PutAsync($"{_address}/api/User/{user.Id}", stringContent);
            httpResponse.EnsureSuccessStatusCode();
            return user;
        }
        public async Task<User> GetByTelegramIdAsync(int telegramId)
        {
            var httpResponse = await httpClient.GetAsync($"{_address}/api/User/{telegramId}");
            httpResponse.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<User>(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
