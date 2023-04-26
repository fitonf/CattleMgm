using CattleMgm.Data.Entities;

namespace CattleMgm.Repository.Humidity
{
    public interface IHumidityRepository
    {
        //Task<List<Data.Entities.CattleHumidity>> CattleHumidity();
        Task<List<Data.Entities.CattleHumidity>> GetHumidity();

    }
}
    