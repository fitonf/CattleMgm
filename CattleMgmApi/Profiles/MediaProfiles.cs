using AutoMapper;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos;

namespace CattleMgmApi.Profiles
{
    public class MediaProfiles : Profile
    {
        public MediaProfiles()
        {
            CreateMap<Media, MediaReadDto>();
            CreateMap<MediaCreateDto, Media>();
            CreateMap<MediaUpdateDto, Media>();
        }
    }
}