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

            CreateMap<ActivityAttendee, AttendeeDTO>()
                .ForMember(p => p.DisplayName, opt => opt.MapFrom(p => p.AppUser.DisplayName))
                .ForMember(p => p.Username, opt => opt.MapFrom(p => p.AppUser.UserName))
                .ForMember(p => p.Bio, opt => opt.MapFrom(p => p.AppUser.Bio))
                .ForMember(d => d.Image, opt => opt.MapFrom(p => p.AppUser.Photos.FirstOrDefault(p => p.IsMain).Url));

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d => d.Image, opt => opt.MapFrom(p => p.Photos.FirstOrDefault(p => p.IsMain).Url));
            

        }
    }
}