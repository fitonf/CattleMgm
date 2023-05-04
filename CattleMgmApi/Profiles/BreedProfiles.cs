using CattleMgmApi.Data.Entities;
using AutoMapper;
using CattleMgmApi.Dtos.BreedDtos;

namespace CattleMgmApi.Profiles
{
    public class BreedProfiles:Profile
    {
        public BreedProfiles()
        {
            //source -> target
            //read
            CreateMap<Breed, BreedReadDto>();
            //create
            CreateMap<BreedCreateDto, Breed>();
            //update
            CreateMap<BreedUpdateDto, Breed>();



        }
    }
}
