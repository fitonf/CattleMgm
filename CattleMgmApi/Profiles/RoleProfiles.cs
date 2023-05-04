using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;

namespace CattleMgmApi.Profiles
{
    public class RoleProfiles:Profile
    {
        public RoleProfiles()
        {
            CreateMap<AspNetRoles, RoleReadDto>();
        }
        
    }
}
