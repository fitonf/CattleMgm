using System.Collections.Generic;
using System.Threading.Tasks;
using CattleMgm.Models;
using CattleMgmApi.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace CattleMgmApi.Repository
{
    public interface IRolesRepository
    {
        Task<List<AspNetRoles>> GetRoles();
        Task<ApplicationRole> CreateRole(string roleName);
        //void UpdateRole(AspNetRoles role);
        Task<AspNetRoles> GetRoleById(string roleId);
        Task<IdentityResult> DeleteRole(string roleId);

        //Task<bool> RoleExists(int id);
        //Task SaveChanges();
    }
}
