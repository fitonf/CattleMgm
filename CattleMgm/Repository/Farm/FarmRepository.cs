using CattleMgm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Repository.Farm
{
    public class FarmRepository : IFarmRepository
    {
        public praktikadbContext _db;

        public FarmRepository(praktikadbContext db)
        {
            _db = db;
        }
        public async Task<List<Data.Entities.Farm>> GetAllFarms(int? farmerId =null)
        {
            var farms = new List<Data.Entities.Farm>();
            if (farmerId == null)
            {
               farms = await _db.Farm
                .Include(x => x.Farmer)
                .ToListAsync();
            }
            else
            {
                farms = await _db.Farm
                .Include(x => x.Farmer)
                .Where(x => x.FarmerId == farmerId)
                .ToListAsync();
            }
            

            return farms;

        }
    }
}
