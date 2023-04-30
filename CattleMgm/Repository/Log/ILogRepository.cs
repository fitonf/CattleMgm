using CattleMgm.Data.Entities;

namespace CattleMgm.Repository.Log
{
    public interface ILogRepository
    {
        List<Data.Entities.Log> GetLogs();
    }
}
