using System.Collections.Generic;
using System.Threading.Tasks;
using CattleMgm.Models;
using CattleMgmApi.Data.Entities;

namespace CattleMgmApi.Repository
{
    public interface IRolesRepository
    {
        Task<List<AspNetRoles>> GetRoles();
        //Task<AspNetRoles> GetRoleById(int id);
        Task<ApplicationRole> CreateRole(string roleName);
        //void UpdateRole(AspNetRoles role);
        //void DeleteRole(AspNetRoles role);
        //Task<bool> RoleExists(int id);
        //Task SaveChanges();
    }
}
