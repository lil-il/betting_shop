using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BettingShop.DataLayer.Models;

namespace BettingShop.DataLayer.DB
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid userId);
        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);
        Task<User> DeleteAsync(Guid Id);
    }
}
