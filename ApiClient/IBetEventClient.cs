using System.Collections.Generic;
using ApiClient.Models;

namespace ApiClient
{
    public interface IApiClient
    {
        IBetEventClient betEvent { get; }
    }

    public interface IBetEventClient
    {
        BetEvent Create(BetEventMeta betEventMeta);

        BetEvent Get(int id);

        BetEvent Delete(int id);

        IEnumerable<BetEvent> GetAll();

        BetEvent Update(BetEvent betEvent);
    }
}
