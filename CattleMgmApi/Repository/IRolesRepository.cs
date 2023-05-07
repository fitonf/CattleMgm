using System.Collections.Generic;
using System.Threading.Tasks;
using CattleMgm.Models;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos.Roles;
using Microsoft.AspNetCore.Identity;

namespace CattleMgmApi.Repository
{
    public interface IRolesRepository
    {
        Task<List<AspNetRoles>> GetRoles();
        Task<ApplicationRole> CreateRole(string roleName);
        Task<ApplicationRole> GetRoleById(string roleId);
        Task<IdentityResult> DeleteRole(string roleId);
        Task<bool> UpdateRole(ApplicationRole role, RolesEditDto model);
        bool RoleExists(int id);
        void SaveChanges();
    }
}
