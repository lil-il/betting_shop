using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetEvent.Api.Models;


namespace betting_shop.database.DB
{
    public interface IEventRepository
    {
        Task<BetEvent.Api.Models.BetEvent[]> GetExistingEvents();
        Task<BetEvent.Api.Models.BetEvent> GetExistingEventById(Guid EventId);
        Task<BetEvent.Api.Models.BetEvent> Create(BetEvent.Api.Models.BetEvent Event);

        Task<BetEvent.Api.Models.BetEvent> Update(BetEvent.Api.Models.BetEvent Event);
        Task<BetEvent.Api.Models.BetEvent> Delete(Guid Id);
    }
}

