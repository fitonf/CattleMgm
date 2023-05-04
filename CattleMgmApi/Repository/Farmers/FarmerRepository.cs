using CattleMgmApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgmApi.Repository.Farmers
{
    public class FarmerRepository : IFarmerRepository
    {
        public PraktikadbContext _context;

        public FarmerRepository(PraktikadbContext context)
        {
            _context = context;
        }

        public async Task CreateFarmer(Farmer farmer)
        {
            if(farmer == null)
            {
                throw new ArgumentNullException(nameof(farmer));
            }
            await _context.Farmer.AddAsync(farmer);
        }

        public void DeleteFarmer(int id)
        {
            var farmer = _context.Farmer.FirstOrDefault(x => x.Id == id);
            if (farmer != null)
            {
                _context.Farmer.Remove(farmer);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Farmer>> GetAllFarmers()
        {
           
            var farmers = await _context.Farmer.ToListAsync();

            return farmers;
        }

        public async Task<Farmer?> GetFarmerById(int id)
        {
            return await _context.Farmer.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(int id, Farmer farmer)
        {
            if (farmer == null)
            {
                throw new ArgumentNullException(nameof(farmer));
            }
            farmer.Id=id;

            _context.Farmer.Update(farmer);
        }
    }
}
