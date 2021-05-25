using System.Collections.Generic;
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

            CreateMap<Client.Models.BetEvent, Models.BetEvent>();

            CreateMap<Models.BetEvent, Client.Models.BetEvent>();

            CreateMap<BetEventMeta, Models.BetEvent>();

            CreateMap<List<string>, List<string>>();


            CreateMap<Client.Models.BetMeta, BetMeta>();

            CreateMap<BetMeta, Client.Models.BetMeta>();

            CreateMap<Client.Models.Bet, Bet>();

            CreateMap<Bet, Client.Models.Bet>();

            CreateMap<BetMeta, Bet>();


            CreateMap<Client.Models.UserMeta, UserMeta>();

            CreateMap<UserMeta, Client.Models.UserMeta>();

            CreateMap<Client.Models.User, User>();

            CreateMap<User, Client.Models.User>();

            CreateMap<UserMeta, User>();
        }
    }
}
