using AutoMapper;
using ExistingEventApi.Models;

namespace ExistingEventApi.Profiles
{
    public class EventsProfile: Profile
    {
        public EventsProfile()
        {
            CreateMap<ApiClient.Models.BetEventMeta, BetEventMeta>();

            CreateMap<BetEventMeta, ApiClient.Models.BetEventMeta>();

            CreateMap<ApiClient.Models.BetEvent, BetEvent>();

            CreateMap<BetEvent, ApiClient.Models.BetEvent>();

            CreateMap<BetEventMeta, BetEvent>();
        }
    }
}
