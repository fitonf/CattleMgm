

using CattleMgm.Data.Entities;

namespace CattleMgm.Repository.CattleTemperature
{
    public interface ICattleTempRepository
    {
       // List<ICattleTempRepository> GetCattleTemperatures();
         Task<List<Data.Entities.CattleTemperature>> GetCattleTemperatures();

       
    }



}
