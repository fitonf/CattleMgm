using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository.Humidity
{
    public interface IHumidityRepository
    {
        Task SaveChanges();

        Task<CattleHumidity?> GetHumidityById(int id);

        Task<IEnumerable<CattleHumidity>> GetAllHumidity(int id);

        Task<IEnumerable<CattleHumidity>> GetAllHumiditys();
        Task CreateHumidity(CattleHumidity humidity);

        void DeleteHumidity(CattleHumidity humidity);
        void UpdateHumidity(CattleHumidity humidity, int id);
    }
}
