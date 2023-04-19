using CattleMgm.Data.Entities;

namespace CattleMgm.Repository.CattleBloodPressures
{
    public interface ICattleBloodPressure
    {
        List<CattleBloodPressure> GetCattleBloodPressure();

        CattleBloodPressure GetCattleBloodPressureById(int id);
    }
}
