using System;
using System.Threading.Tasks;
using BettingShop.Api.Client.Models;

namespace BettingShop.Api.Client
{
    public interface IUserClient
    {
        Task<User> CreateAsync(UserMeta userMeta);

        Task<User> GetAsync(Guid id);

        Task<User> DeleteAsync(Guid id);

        Task<User[]> GetAllAsync();

        Task<User> UpdateAsync(User user);
    }
}
