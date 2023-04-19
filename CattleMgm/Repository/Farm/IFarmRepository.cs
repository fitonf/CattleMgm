namespace CattleMgm.Repository.Farm
{
    public interface IFarmRepository
    {
        Task<List<Data.Entities.Farm>> GetAllFarms(int? farmerId = null);
    }
}
