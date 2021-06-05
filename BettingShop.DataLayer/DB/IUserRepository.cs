using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BettingShop.DataLayer.Models;

namespace BettingShop.DataLayer.DB
{
    public interface IUserRepository
    {
        Task<User[]> GetAllAsync();
        Task<User> GetByIdAsync(Guid userId);
        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);
        Task<User> DeleteAsync(Guid Id);
    }
}
