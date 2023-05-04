using CattleMgm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Repository.Cattles
{
    public class CattleRepository : ICattleRepository
    {
        private praktikadbContext _context;

        public CattleRepository(praktikadbContext context)
        {
            _context = context;
        }

        public bool AddCattles(List<Cattle> cattles)
        {
            throw new NotImplementedException();
        }

       

        public List<Cattle> GetCattles()
        {
            var cattles = _context.Cattle
                .Include(x => x.Farm)
                .ThenInclude(x => x.Farmer)
                .Include(x => x.Breed)
                .Include(x=>x.Municipality)
                .Include(x => x.CreatedByNavigation)
                .ToList();

            return cattles;
        }

        public List<Cattle> GetCattlesByFarmerId(int farmerId)
        {
            var farm = _context.Farm.Where(x=>x.FarmerId == farmerId).FirstOrDefault();
            var cattles = _context.Cattle
                .Include(x => x.Farm)
                .ThenInclude(x => x.Farmer)
                .Include(x => x.Breed)
                .Include(x => x.Municipality)
                .Where(x=>x.FarmId == farm.Id)
                .ToList();

            return cattles;
        }
    }
}
