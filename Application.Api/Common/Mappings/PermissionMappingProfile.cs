using Application.Api.Common.Models;
using AutoMapper;
using Domain.Api.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Api.Common.Mappings
{
    public class PermissionMappingProfile : Profile
    {
        public PermissionMappingProfile()
        {
            CreateMap<Permission, PermissionGetDto>().ReverseMap();
        }
    }
}
