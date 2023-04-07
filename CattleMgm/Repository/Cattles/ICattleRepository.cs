using CattleMgm.Data.Entities;

namespace CattleMgm.Repository.Cattles
{
    public interface ICattleRepository
    {
        List<Cattle> GetCattles();

        bool AddCattles(List<Cattle> cattles);
    }
}
