using System.Collections.Generic;
using AutoMapper;
using BettingShop.DataLayer.Models;

namespace BetEvent.Api.Profiles
{
    public class EventsProfile : Profile
    {
        public EventsProfile()
        {
            CreateMap<Client.Models.BetEventMeta, BetEventMeta>();

            CreateMap<BetEventMeta, Client.Models.BetEventMeta>();

            CreateMap<Client.Models.BetEvent, BettingShop.DataLayer.Models.BetEvent>();

            CreateMap<BettingShop.DataLayer.Models.BetEvent, Client.Models.BetEvent>();

            CreateMap<BetEventMeta, BettingShop.DataLayer.Models.BetEvent>();

            CreateMap<List<string>, List<string>>();
        }
    }
}
