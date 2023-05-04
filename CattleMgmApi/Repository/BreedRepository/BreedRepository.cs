using CattleMgmApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgmApi.Repository.BreedRepository
{
    public class BreedRepository : IBreedRepository
    { 
        public PraktikadbContext _context;
        public BreedRepository(PraktikadbContext context)
        {
            _context = context;
        }
        public async Task CreateBreed(Breed breed)
        {
            if (breed == null)
            {
                throw new ArgumentNullException(nameof(breed));
            }
            //insertimi ne databaze me EFCore

            await _context.Breed.AddAsync(breed);
        }
        public void UpdateBreed(Breed breed, int id)
        {
            if (breed == null)
            {
                throw new ArgumentNullException(nameof(breed));
            }

            breed.Id = id;

            _context.Breed.Update(breed);
        }
        public void DeleteBreed(Breed breed)
        {
            if (breed == null)
                throw new ArgumentNullException(nameof(breed));

            //fshirja nga db me EFCore
            _context.Breed.Remove(breed);
        }

        

        public async Task<Breed?> GetBreedById(int id)
        {
            //gjetja e nje gjedhe sipas id's se caktuar
            return await _context.Breed.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChanges()
        {
            // ruajtja ne databaze
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Breed>> GetAllBreed()
        {
            //listimi i tabeles me EFCore
            var breed = await _context.Breed.ToListAsync();

            return breed;
        }

        Task IBreedRepository.DeleteBreed(Breed breed)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBreed(Breed existing_breed)
        {
            throw new NotImplementedException();
        }
    }
}
