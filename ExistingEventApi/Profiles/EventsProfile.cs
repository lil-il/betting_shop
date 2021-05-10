using AutoMapper;
using ExistingEventApi.DTOs;
using ExistingEventApi.Models;

namespace ExistingEventApi.Profiles
{
    public class EventsProfile: Profile
    {
        public EventsProfile()
        {
            CreateMap<ExistingEvent, EventReadDTO>();
            CreateMap<EventCreateDTO, ExistingEvent>();
            CreateMap<EventUpdateDTO, ExistingEvent>();
        }
    }
}
