using AutoMapper;
using CattleMgm.Models;
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
            CreateMap<RolesCreateDto, ApplicationRole>();
            //update
            CreateMap<RolesEditDto, ApplicationRole>();

        }
    }
}
