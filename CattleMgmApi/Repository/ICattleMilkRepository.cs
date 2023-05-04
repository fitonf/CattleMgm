using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository
{
    public interface ICattleMilkRepository
    {
        Task SaveChanges();

        Task<CattleMilk?> GetCattleMilkById(int id);

        Task<CattleMilk?> GetLastCattleMilk(int id);

        Task<IEnumerable<CattleMilk>> GetAllCattlesMilk();

        Task CreateCattleMilk(CattleMilk cattlemilk);

        void DeleteCattleMilk(CattleMilk cattlemilk);
        void UpdateCattleMilk(CattleMilk cattlemilk, int Id);
        
    }
}
