using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetEvent.Api.Models;


namespace BettingShop.DataBase.DB
{
    public interface IEventRepository
    {
        Task<BetEvent.Api.Models.BetEvent[]> GetExistingEventsAsync();
        Task<BetEvent.Api.Models.BetEvent> GetExistingEventByIdAsync(Guid EventId);
        Task<BetEvent.Api.Models.BetEvent> CreateAsync(BetEvent.Api.Models.BetEvent Event);

        Task<BetEvent.Api.Models.BetEvent> UpdateAsync(BetEvent.Api.Models.BetEvent Event);
        Task<BetEvent.Api.Models.BetEvent> DeleteAsync(Guid Id);
    }
}

