using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository
{
    public interface ICattleRepository
    {
        Task SaveChanges();

        Task<Cattle?> GetCattleById(int id);

        Task<IEnumerable<Cattle>> GetAllCattles();

        Task CreateCattle(Cattle cattle);

        void DeleteCattle(Cattle cattle);
    }
}
