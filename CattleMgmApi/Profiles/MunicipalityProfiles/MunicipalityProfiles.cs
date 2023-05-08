using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos.MunicipalyDtos;
using CattleMgmApi.Dtos;

namespace CattleMgmApi.Profiles
{
    public class MunicipalityProfiles : Profile
    {
        public MunicipalityProfiles()
        {
            //source -> target
            //read
            CreateMap<Municipality, MunicipalityReadDto>();
            //create
            CreateMap<MunicipalityCreateDto, Municipality>();
            //update
            CreateMap<MunicipalityUpdateDto, Municipality>();



        }
    }

   
}

