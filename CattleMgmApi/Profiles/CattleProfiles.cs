using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;

namespace CattleMgmApi.Profiles
{
    public class CattleProfiles : Profile
    {
        public CattleProfiles()
        {
            //source -> target
            //read
            CreateMap<Cattle, CattleReadDto>();
            //create
            CreateMap<CattleCreateDto, Cattle>();
            //update
            CreateMap<CattleUpdateDto, Cattle>();
            //Delete
            CreateMap<CattleDeleteDto, Cattle>();


        }
    }
}
