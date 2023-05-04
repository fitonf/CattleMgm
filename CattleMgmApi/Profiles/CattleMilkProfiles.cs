using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;

namespace CattleMgmApi.Profiles
{
    public class CattleMilkProfiles : Profile
    {
        public CattleMilkProfiles()
        {
            //source -> target
            //read
            CreateMap<CattleMilk, CattleMilkReadDto>();
            //create
            CreateMap<CattleMilkCreateDto, CattleMilk>();
            //update
            CreateMap<CattleMilkUpdateDto, CattleMilk>();

            CreateMap<CattleMilkDeleteDto, CattleMilk>();



        }
    }
}
