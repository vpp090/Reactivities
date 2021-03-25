using System.Linq;
using Application.Activities;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDTO>()
                .ForMember(d => d.HostUsername, opt => opt.MapFrom(a => a.Attendees.FirstOrDefault(a => a.IsHost).AppUser.UserName));

            CreateMap<ActivityAttendee, Profiles.Profile>()
                .ForMember(p => p.Displayname, opt => opt.MapFrom(p => p.AppUser.DisplayName))
                .ForMember(p => p.Username, opt => opt.MapFrom(p => p.AppUser.UserName))
                .ForMember(p => p.Bio, opt => opt.MapFrom(p => p.AppUser.Bio));
        }
    }
}