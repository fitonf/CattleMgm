using CattleMgmApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CattleMgmApi.Repository.CattlePositionRepository
{
    public class PositionRepository : IPositionRepository
    {
        public PraktikadbContext _context;
        public PositionRepository(PraktikadbContext context)
        {
            _context = context;
        }
        public async Task CreatePosition(CattlePosition position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }
            //insertimi ne databaze me EFCore

            await _context.CattlePosition.AddAsync(position);
        }

        public void UpdatePosition(CattlePosition position, int id)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            position.Id = id;

            _context.CattlePosition.Update(position);
        }

        public async Task<IEnumerable<CattlePosition>> GetAllPositions()
        {
            //listimi i tabeles me EFCore
            var pos = await _context.CattlePosition.ToListAsync();

            return pos;
        }

        public async Task<CattlePosition?> GetPositionById(int id)
        {
            //gjetja e nje gjedhe sipas id's se caktuar
            return await _context.CattlePosition.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChanges()
        {
            // ruajtja ne databaze
            await _context.SaveChangesAsync();
        }
    }
}
