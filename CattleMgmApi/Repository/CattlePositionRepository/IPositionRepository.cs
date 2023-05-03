using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository.CattlePositionRepository
{
    public interface IPositionRepository
    {
        Task SaveChanges();

        Task<CattlePosition?> GetPositionById(int id);

        Task<CattlePosition?> GetLastPosition(int id);

        Task<IEnumerable<CattlePosition>> GetAllPositions();

        Task CreatePosition(CattlePosition position);

        void UpdatePosition(CattlePosition position, int Id);
    }
}
