using CattleMgm.Data.Entities;
using CattleMgm.Repository.Farm;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Repository.Position
{
    public class PositionRepository : IPositionRepository
    {

        public praktikadbContext _db;
        public PositionRepository(praktikadbContext db)
        {
            _db = db;
        }

        public async Task<List<Data.Entities.CattlePosition>> GetAllPositions()
        {
            var positions = new List<Data.Entities.CattlePosition>();
            positions = await _db.CattlePosition
                 .Include(x => x.Cattle)
                 .ToListAsync();

            return positions;

        }
    }
}
