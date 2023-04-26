using CattleMgm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Repository.Milk
{
    public class MilkRepository : IMilkRepository
    {
        public praktikadbContext _db;
        public MilkRepository(praktikadbContext db)
        {
            _db = db;
        }

        public async Task<List<CattleMilk>> GetAllMilk(int farmerId)
        {
            var farm = await _db.Farm.Where(x => x.FarmerId == farmerId).FirstOrDefaultAsync();

            var milkCollected = await _db.CattleMilk.Where(x=>x.Cattle.FarmId == farm.Id).ToListAsync();

            return milkCollected;
        }
    }
}
