using CattleMgm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Repository.CattleTemperature
{
    public class CattleTempRepository : ICattleTempRepository
    {
        private praktikadbContext _context;
    
    public CattleTempRepository(praktikadbContext context)
        {
            _context = context;
        }

        public async Task<List<Data.Entities.CattleTemperature>> GetCattleTemperatures()
        {
            var temp = new List<Data.Entities.CattleTemperature>();
            //if (cattleId == null)
            //{
                temp = await _context.CattleTemperature
                 .Include(x => x.Cattle)
                 .ToListAsync();
           // }
            //else
            //{
            //    temp = await _context.CattleTemperature
            //    .Include(x => x.Cattle)
            //    .Where(x => x.Id == cattleId)
            //    .ToListAsync();
            //}


            return temp;


        }


        //public List<ICattleTempRepository> GetCattleTemperatures()
        //{
        //    throw new NotImplementedException();
        //}


    }
}
