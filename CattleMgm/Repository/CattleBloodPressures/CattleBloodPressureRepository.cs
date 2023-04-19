using CattleMgm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Repository.CattleBloodPressures;
    public class CattleBloodPressureRepository : ICattleBloodPressure
    {

        private praktikadbContext _context;

        public CattleBloodPressureRepository(praktikadbContext context)
        {
            _context = context;
        }
        public List<CattleBloodPressure> GetCattleBloodPressure()
        {
        
        var cattles = _context.CattleBloodPressure
               .Include(x => x.Cattle)
               .ToList();

        return cattles;
        }

    public CattleBloodPressure GetCattleBloodPressureById(int id)
    {

        var cattle = _context.CattleBloodPressure
               .Include(x => x.Cattle).Where(x => x.CattleId == id).FirstOrDefault();

        return cattle;
    }
}

