using AutoMapper;
using ExistingEventApi.Models;

namespace ExistingEventApi.Profiles
{
    public class EventsProfile: Profile
    {
        public EventsProfile()
        {
            CreateMap<ApiClient.Models.BetEventMeta, BetEventMeta>();

            CreateMap<ApiClient.Models.BetEvent, BetEvent>();

            CreateMap<BetEventMeta, BetEvent>();
        }
    }
}
