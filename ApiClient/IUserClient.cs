using System;
using System.Threading.Tasks;
using BetEvent.Api.Client.Models;

namespace BetEvent.Api.Client
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
