using System.Linq;
using AutoMapper;
using EldritchDating.API.DTOs;
using EldritchDating.API.Models;

namespace EldritchDating.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => 
                    s.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(d => d.AccountAge, o => o.MapFrom(s => s.Created.CalculateAge()));
            CreateMap<User, UserForDetailDto>()
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => 
                    s.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(d => d.AccountAge, o => o.MapFrom(s => s.Created.CalculateAge()));
            CreateMap<Photo, PhotoForDetailDto>();
            CreateMap<UserForUpdateDto, User>();
        }
    }
}