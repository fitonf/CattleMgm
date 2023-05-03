using CattleMgmApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace CattleMgmApi.Repository.Humidity

{
    public class HumidityRepository : IHumidityRepository
    {
        public PraktikadbContext _context;
        public HumidityRepository(PraktikadbContext context)
        {
            _context = context;
        }
        public async Task CreateHumidity(CattleHumidity humidity)
        {
            if (humidity == null)
            {
                throw new ArgumentNullException(nameof(humidity));
            }
            //insertimi ne databaze me EFCore

            await _context.CattleHumidity.AddAsync(humidity);
        }

        public void DeleteHumidity(CattleHumidity humidity)
        {
            if (humidity == null)
                throw new ArgumentNullException(nameof(humidity));

            //fshirja nga db me EFCore
            _context.CattleHumidity.Remove(humidity);
        }

        public async Task<IEnumerable<CattleHumidity>> GetAllHumidity()
        {
            //listimi i tabeles me EFCore
            var humiditys = await _context.CattleHumidity.ToListAsync();

            return humiditys;
        }

        public async Task<CattleHumidity?> GetHumidityById(int id)
        {
            //gjetja e nje gjedhe sipas id's se caktuar
            return await _context.CattleHumidity.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChanges()
        {
            // ruajtja ne databaze
            await _context.SaveChangesAsync();
        }
    }
}

