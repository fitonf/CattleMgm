using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository 

{
    public interface IFarmerRepository
    {
        Task SaveChanges();

        Task<Farmer?> GetFarmerById(int id);

       // IList<Farmer> Get();
        Task<IEnumerable<Farmer>> GetAllFarmers();

        Task CreateFarmer(Farmer farmer);
        void Update(int id, Farmer farmer);
        void DeleteFarmer(int id);
    }
}
