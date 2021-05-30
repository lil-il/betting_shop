using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BettingShop.DataLayer.Models;


namespace BettingShop.DataLayer.DB
{
    public interface IEventRepository
    {
        Task<BetEvent[]> GetExistingEventsAsync();
        Task<BetEvent> GetExistingEventByIdAsync(Guid EventId);
        Task<BetEvent> CreateAsync(BetEvent Event);

        Task<BetEvent> UpdateAsync(BetEvent Event);
        Task<BetEvent> DeleteAsync(Guid Id);
    }
}

