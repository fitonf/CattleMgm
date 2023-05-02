using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos.CattlePositionDtos;

namespace CattleMgmApi.Profiles.CattlePositionProfiles
{
    public class PositionProfiles : Profile
    {
        public PositionProfiles()
        {
            //source -> target
            //read
            CreateMap<CattlePosition, PositionReadDto>();
            //create
            CreateMap<PositionCreateDto, CattlePosition>();
            //update
            CreateMap<PositionUpdateDto, CattlePosition>();



        }
    }
}
