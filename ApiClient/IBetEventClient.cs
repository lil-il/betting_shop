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
        Task<BetEvent> Create(BetEventMeta betEventMeta);

        Task<BetEvent> Get(int id);

        Task<BetEvent> Delete(int id);

        Task<BetEvent[]> GetAll();

        Task<BetEvent> Update(BetEvent betEvent);
    }
}
