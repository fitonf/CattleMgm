using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos.TempDtos;

namespace CattleMgmApi.Profiles.TempProfiles
{
    public class CattleTempProfiles :Profile
    {
        public CattleTempProfiles()
        {
            //source -> target
            //read
            CreateMap<CattleTemperature, CattleTempReadDto>();
            //create
            CreateMap<CattleTempCreateDto, CattleTemperature>();
            //update
            CreateMap<CattleTempUpdateDto, CattleTemperature>();

            CreateMap<CattleTempDeleteDto, CattleTemperature>();


        }
    }
}
