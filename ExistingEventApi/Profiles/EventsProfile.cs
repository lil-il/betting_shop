using System.Collections.Generic;
using AutoMapper;
using BetEvent.Api.Models;

namespace BetEvent.Api.Profiles
{
    public class EventsProfile: Profile
    {
        public EventsProfile()
        {
            CreateMap<Client.Models.BetEventMeta, BetEventMeta>();

            CreateMap<BetEventMeta, Client.Models.BetEventMeta>();

            CreateMap<Client.Models.BetEvent, Models.BetEvent>();

            CreateMap<Models.BetEvent, Client.Models.BetEvent>();

            CreateMap<BetEventMeta, Models.BetEvent>();

            CreateMap<List<string>, List<string>>();
        }
    }
}
