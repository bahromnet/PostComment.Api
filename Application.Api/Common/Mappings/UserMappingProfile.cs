using Application.Api.Common.Models;
using AutoMapper;
using Domain.Api.Entities;

namespace Application.Api.Common.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserGetDto>().ReverseMap();
        }
    }
}
