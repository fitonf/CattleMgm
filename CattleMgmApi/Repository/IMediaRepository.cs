using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository
{
    public interface IMediaRepository
    {
        Task SaveChanges();

        Task<IEnumerable<Data.Entities.Media>> GetAllMedia();

        Task CreateMedia(Data.Entities.Media media);

        void DeleteMedia(Data.Entities.Media media);
    }
}