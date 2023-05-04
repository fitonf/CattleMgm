using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository.BreedRepository
{
    public interface IBreedRepository
    {
        Task SaveChanges();

        Task<Breed?> GetBreedById(int id);

        Task<IEnumerable<Breed>> GetAllBreed();

        Task CreateBreed(Breed breed);

        Task DeleteBreed(Breed breed);
        void UpdateBreed(Breed mapped_object, int Id);
        Task UpdateBreed(Breed existing_breed);
    }
}
