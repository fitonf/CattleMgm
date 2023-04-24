using CattleMgm.Data.Entities;

namespace CattleMgm.Repository.CattleBloodPressures
{
    public interface ICattleBloodPressureRepository
    {
        List<CattleBloodPressure> GetCattleBloodPressures();
       
    }
}
