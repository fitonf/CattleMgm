using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository
{
    public interface ICattleRepository
    {
        Task SaveChanges();

        Task<Cattle?> GetCattleById(int id);

        Task<IEnumerable<Cattle>> GetAllCattles();

        Task CreateCattle(Cattle cattle);

        void DeleteCattle(int id);

        //Task UpdateCattle(Cattle cattle);
        //void UpdateCattle(int id, Cattle mapped_object);
        //void UpdateCattle(int id, Cattle mapped_object);

        void UpdateCattle(Cattle cattle, int id);
        
       


    }
}
