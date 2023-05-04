using CattleMgmApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



namespace CattleMgmApi.Repository
{
    public class CattleMilkRepository : ICattleMilkRepository
    {
        public PraktikadbContext _context;
        public CattleMilkRepository(PraktikadbContext context)
        {
            _context = context;
        }
        public async Task CreateCattleMilk(CattleMilk cattlemilk)
        {
            if (cattlemilk == null)
            {
                throw new ArgumentNullException(nameof(cattlemilk));
            }
            //insertimi ne databaze

            await _context.CattleMilk.AddAsync(cattlemilk);
        }
        public void DeleteCattleMilk(CattleMilk cattlemilk)
        {
            if (cattlemilk == null)
                throw new ArgumentNullException(nameof(cattlemilk));

            //fshirja nga db 
            _context.CattleMilk.Remove(cattlemilk);
        }
        public async Task<IEnumerable<CattleMilk>> GetAllCattlesMilk()
        {
            //listimi i tabeles
            var cattles = await _context.CattleMilk.ToListAsync();

            return cattles;
        }
        public async Task<CattleMilk?> GetCattleMilkById(int id)
        {
            //gjetja e qumshtit sipas id's se caktuar
            return await _context.CattleMilk.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CattleMilk?> GetLastCattleMilk(int id)
        {
            return await _context.CattleMilk.Where(x => x.Id == id).OrderByDescending(x => x.Created).FirstOrDefaultAsync();
        }
      
        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCattleMilk(CattleMilk cattlemilk, int Id)
        {
            throw new NotImplementedException();
        }

     
    }
}
