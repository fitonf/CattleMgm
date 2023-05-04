using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos.Roles;

namespace CattleMgmApi.Profiles
{
    public class RolesProfiles : Profile
    {
        public RolesProfiles()
        {
            //source -> target
            //read
            CreateMap<AspNetRoles, RolesReadDto>();
            //create
            CreateMap<RolesCreateDto, AspNetRoles>();
            //update
            CreateMap<RolesEditDto, AspNetRoles>();

        }
    }
}
