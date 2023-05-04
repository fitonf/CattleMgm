using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository
{
    public interface IRoleRepository
    {
        Task<List<AspNetRoles>> GetRoles();
    }
}
