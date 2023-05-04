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

            await _context.CattleHumidity.AddAsync(humidity);
        }
        public void UpdateHumidity(CattleHumidity humidity, int id)
        {
            if (humidity == null)
            {
                throw new ArgumentNullException(nameof(humidity));
            }
            humidity.Id = id;

            _context.CattleHumidity.Update(humidity);
        }

        public void DeleteHumidity(CattleHumidity humidity)
        {
            if (humidity == null)
                throw new ArgumentNullException(nameof(humidity));


            _context.CattleHumidity.Remove(humidity);
        }

        public async Task<IEnumerable<CattleHumidity>> GetAllHumidity()///
        {
            var humidity = await _context.CattleHumidity.ToListAsync();

            return humidity;
        }
        
      

        public async Task<CattleHumidity?> GetHumidityById(int id)
        {
            return await _context.CattleHumidity.FirstOrDefaultAsync(x => x.Id == id);
        }

       

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        
    }
}