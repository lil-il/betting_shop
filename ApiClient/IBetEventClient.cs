using System.Collections.Generic;
using System.Threading.Tasks;
using ApiClient.Models;

namespace ApiClient
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
