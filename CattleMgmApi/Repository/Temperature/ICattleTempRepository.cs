using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository.Temperature
{
    public interface ICattleTempRepository
    {
        Task SaveChanges();

        Task<CattleTemperature?> GetCattleTempById(int id);

        Task<IEnumerable<CattleTemperature>> GetAllCattlesTemp();

        Task CreateCattleTemp(CattleTemperature cattleTemp);

        void DeleteCattleTemp(CattleTemperature cattleTemp);

        void UpdateCattleTemp(CattleTemperature cattleTemp, int id);
    }
}
