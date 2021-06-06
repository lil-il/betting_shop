using AutoMapper;
using BettingShop.Api.Models;

namespace BettingShop.Api.Profiles
{
    public class EventsProfile: Profile
    {
        public EventsProfile()
        {
            CreateMap<Client.Models.BetEventMeta, DataLayer.Models.BetEventMeta>();

            CreateMap<DataLayer.Models.BetEventMeta, Client.Models.BetEventMeta>();

            CreateMap<Client.Models.BetEvent, BettingShop.DataLayer.Models.BetEvent>();

            CreateMap<BettingShop.DataLayer.Models.BetEvent, Client.Models.BetEvent>();

            CreateMap<DataLayer.Models.BetEventMeta, DataLayer.Models.BetEvent>();


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
