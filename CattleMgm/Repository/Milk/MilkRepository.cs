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

        public async Task<List<CattleMilk>> GetAllMilk()
        {

            var milk= new List<CattleMilk>();

            milk=await _db.CattleMilk.Include(x=>x.Cattle).ToListAsync();

            return milk;
        }
    }
}
