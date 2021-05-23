using System;
using System.Threading.Tasks;
using BetEvent.Api.Client.Models;

namespace BetEvent.Api.Client
{
    public interface IBetClient
    {
        Task<Bet> CreateAsync(BetMeta betMeta);

        Task<Bet> GetAsync(Guid id);

        Task<Bet> DeleteAsync(Guid id);

        Task<Bet[]> GetAllAsync();

        Task<Bet> UpdateAsync(Bet bet);
    }
}
