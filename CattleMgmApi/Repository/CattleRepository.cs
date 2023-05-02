using CattleMgmApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace CattleMgmApi.Repository
{
    public class CattleRepository : ICattleRepository
    {
        public PraktikadbContext _context;
        public CattleRepository(PraktikadbContext context)
        {
            _context = context;
        }
        public async Task CreateCattle(Cattle cattle)
        {
            if(cattle == null)
            {
                throw new ArgumentNullException(nameof(cattle));
            }
            //insertimi ne databaze me EFCore

            await _context.Cattle.AddAsync(cattle);
        }

        public void DeleteCattle(Cattle cattle)
        {
            if(cattle == null)
                throw new ArgumentNullException(nameof(cattle));

            //fshirja nga db me EFCore
            _context.Cattle.Remove(cattle);
        }

        public async Task<IEnumerable<Cattle>> GetAllCattles()
        {
            //listimi i tabeles me EFCore
            var cattles = await _context.Cattle.ToListAsync();

            return cattles;
        }

        public async Task<Cattle?> GetCattleById(int id)
        {
            //gjetja e nje gjedhe sipas id's se caktuar
            return await _context.Cattle.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChanges()
        {
            // ruajtja ne databaze
            await _context.SaveChangesAsync();
        }
    }
}
