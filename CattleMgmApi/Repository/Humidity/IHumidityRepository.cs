using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository.Humidity
{
    public interface IHumidityRepository
    {
        Task SaveChanges();

        Task<CattleHumidity?> GetHumidityById(int id);

        Task<IEnumerable<CattleHumidity>> GetAllHumidity();

        Task CreateHumidity(CattleHumidity humidity);

        Task<CattleHumidity?> GetLastHumidity(int id);

        Task DeleteHumidity(CattleHumidity humidity);
        void UpdateHumidity(CattleHumidity mapped_object, int Id);
        Task UpdateHumidity(CattleHumidity existing_humidity);
        //void DeleteCattle(CattleHumidity humidity);
    }
}
