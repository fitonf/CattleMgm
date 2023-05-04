using CattleMgmApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgmApi.Repository.Temperature
{
    public class CattleTempRepository :ICattleTempRepository
    {

        public PraktikadbContext _context;
        public CattleTempRepository(PraktikadbContext context)
        {
            _context = context;
        }

        public async Task CreateCattleTemp(CattleTemperature cattleTemp)
        {
            if (cattleTemp == null)
            {
                throw new ArgumentNullException(nameof(cattleTemp));
            }
            //insertimi ne databaze me EFCore

            await _context.CattleTemperature.AddAsync(cattleTemp);
        }

        public void DeleteCattleTemp(CattleTemperature cattleTemp)
        {
            if (cattleTemp == null)
                throw new ArgumentNullException(nameof(cattleTemp));

            
            //fshirja nga db me EFCore
            _context.CattleTemperature.Remove(cattleTemp);
        }

        public async Task<IEnumerable<CattleTemperature>> GetAllCattlesTemp()
        {
            //listimi i tabeles me EFCore
            var cattlesTemp = await _context.CattleTemperature.ToListAsync();

            return cattlesTemp;
        }

        public async Task<CattleTemperature?> GetCattleTempById(int id)
        {
            //gjetja e nje gjedhe sipas id's se caktuar
            return await _context.CattleTemperature.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateCattleTemp(CattleTemperature cattleTemp,int id) 
        {
            if (cattleTemp == null)
            {
                throw new ArgumentNullException(nameof(cattleTemp));
            }

            cattleTemp.Id = id;

            _context.CattleTemperature.Update(cattleTemp);

        }

        public async Task SaveChanges()
        {
            // ruajtja ne databaze
            await _context.SaveChangesAsync();
        }


    }
}
