using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;
using CattleMgmApi.Dtos.Farmer;

namespace CattleMgmApi.Profiles.Farmers
{
    public class FarmerProfile: Profile
    {
        public FarmerProfile()
        {  
         //create
        CreateMap<FarmerCreateDto, Farmer>();

        CreateMap<Farmer, FarmerReadDto>();

        CreateMap<FarmerUpdateDto, Farmer>();

         CreateMap<FarmerDeleteDto, Farmer>();


        }

    }
}
