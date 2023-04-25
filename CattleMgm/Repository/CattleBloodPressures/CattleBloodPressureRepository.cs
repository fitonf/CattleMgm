using CattleMgm.Data.Entities;
using CattleMgm.Repository.CattleBloodPressures;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Repository.Cattles
{
    public class CattleBloodPressureRepository : ICattleBloodPressureRepository
    {
        private praktikadbContext _context;

        public CattleBloodPressureRepository (praktikadbContext context)
        {
            _context = context;
        }

        public List<CattleBloodPressure> GetCattleBloodPressures()
        {
            var cattles = _context.CattleBloodPressure
                .Include(x => x.Cattle).ToList();                

            return cattles;
        }
    }
}
