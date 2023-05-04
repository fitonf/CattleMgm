using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository.Humidity
{
    public interface IHumidityRepository
    {
        Task SaveChanges();

        Task<CattleHumidity?> GetHumidityById(int id);

        Task<IEnumerable<CattleHumidity>> GetAllHumiditys();

        Task CreateHumidity(CattleHumidity humidity);

        Task<CattleHumidity?> GetLastHumidity(int id);
        void UpdateHumidity(CattleHumidity humidity, int Id);
        //void DeleteCattle(CattleHumidity humidity);
    }
}
