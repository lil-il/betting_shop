using AutoMapper;
using BettingShop.Api.Models;

namespace BettingShop.Api.Profiles
{
    public class EventsProfile: Profile
    {
        public EventsProfile()
        {
            CreateMap<Client.Models.BetEventMeta, BetEventMeta>();

            CreateMap<BetEventMeta, Client.Models.BetEventMeta>();

            CreateMap<Client.Models.BetEvent, BetEvent>();

            CreateMap<BetEvent, Client.Models.BetEvent>();

            CreateMap<BetEventMeta, BetEvent>();
        }
    }
}
