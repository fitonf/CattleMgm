namespace CattleMgm.Repository.Position
{
    public interface IPositionRepository
    {
        Task<List<Data.Entities.CattlePosition>> GetAllPositions();
    }
}
