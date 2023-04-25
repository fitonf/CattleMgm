using CattleMgm.Data.Entities;

namespace CattleMgm.Repository.Milk
{
    public interface IMilkRepository
    {
        Task<List<CattleMilk>> GetAllMilk(int farmerId);
    }
}
