using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BettingShop.DataLayer.Models;

namespace BettingShop.DataLayer.DB
{
    public interface IBetRepository
    {
        Task<Bet[]> GetAllAsync();
        Task<Bet> GetByIdAsync(Guid betId);
        Task<Bet> CreateAsync(Bet bet);

        Task<Bet> UpdateAsync(Bet bet);
        Task<Bet> DeleteAsync(Guid Id);
    }
}
