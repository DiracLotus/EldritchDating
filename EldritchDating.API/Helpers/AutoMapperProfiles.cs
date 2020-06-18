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
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt => opt
                    .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(m => m.RecipientPhotoUrl, opt => opt
                    .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
        }
    }
}