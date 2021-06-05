using AutoMapper;
using BettingShop.Api.Client.Models;

namespace BettingShop.Api.Profiles
{
    public class EventsProfile : Profile
    {
        public EventsProfile()
        {
            CreateMap<Client.Models.BetEventMeta, DataLayer.Models.BetEventMeta>();

            CreateMap<DataLayer.Models.BetEventMeta, Client.Models.BetEventMeta>();

            CreateMap<Client.Models.BetEvent, DataLayer.Models.BetEvent>();

            CreateMap<DataLayer.Models.BetEvent, Client.Models.BetEvent>();

            CreateMap<DataLayer.Models.BetEventMeta, DataLayer.Models.BetEvent>();


            CreateMap<Client.Models.BetMeta, DataLayer.Models.BetMeta>();

            CreateMap<DataLayer.Models.BetMeta, Client.Models.BetMeta>();

            CreateMap<Client.Models.Bet, DataLayer.Models.Bet>();

            CreateMap<DataLayer.Models.Bet, Client.Models.Bet>();

            CreateMap<DataLayer.Models.BetMeta, DataLayer.Models.Bet>();


            CreateMap<Client.Models.UserMeta, DataLayer.Models.UserMeta>();

            CreateMap<DataLayer.Models.UserMeta, Client.Models.UserMeta>();

            CreateMap<Client.Models.User, DataLayer.Models.User>();

            CreateMap<DataLayer.Models.User, Client.Models.User>();

            CreateMap<DataLayer.Models.UserMeta, DataLayer.Models.User>();
        }
    }
}
