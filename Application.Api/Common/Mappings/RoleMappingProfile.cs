using Application.Api.Common.Models;
using AutoMapper;
using Domain.Api.IdentityEntity;

namespace Application.Api.Common.Mappings;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Role, RoleGetDto>().ReverseMap();
    }
}
