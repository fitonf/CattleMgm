using AutoMapper;
using CattleMgmApi.Dtos;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos.Humidity;

namespace CattleMgmApi.Profiles
{
    public class HumidityProfiles : Profile
    {
        public HumidityProfiles() 
        {
            //source -> target
            //read
            CreateMap<CattleHumidity, HumidityReadDto>();
            //create
            CreateMap<HumidityCreateDto, CattleHumidity>();
            //update
            CreateMap<HumidityUpdateDto, CattleHumidity>();
            
        }
    }
}

