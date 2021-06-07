using System;
using System.Threading.Tasks;
using BettingShop.Api.Client.Models;

namespace BettingShop.Api.Client
{
    public interface IApiClient
    {
        IBetEventClient betEvent { get; }
    }

    public interface IBetEventClient
    {
        Task<Models.BetEvent> CreateAsync(BetEventMeta betEventMeta);

        Task<Models.BetEvent> GetAsync(Guid id);

        Task<Models.BetEvent> DeleteAsync(Guid id);

        Task<Models.BetEvent[]> GetAllAsync();

        Task<Models.BetEvent> UpdateAsync(Models.BetEvent betEvent);

        Task<Models.BetEvent> CloseEventAsync(Guid id, string winOutcome);

        Task<Models.BetEvent[]> GetAllEventsFromCreatorAsync(long creatorId);
    }
}
