using CattleMgm.Models; // The ApplicationRole model is defined here.
using CattleMgmApi.Data.Entities; // The AspNetRoles model is defined here.
using Microsoft.AspNetCore.Identity; // The RoleManager class is defined here.
using Microsoft.EntityFrameworkCore; // The DbContext class is defined here.

namespace CattleMgmApi.Repository
{
    public class RoleRepository : IRolesRepository
    {
        // RoleManager naj qasje ne metodat per te menaxhuar rolet
        private RoleManager<ApplicationRole> _roleManager;
        // PraktikaDbContext jep qasje ne Databaze.
        private PraktikadbContext _context; 

        public RoleRepository(RoleManager<ApplicationRole> roleManager, PraktikadbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<ApplicationRole> CreateRole(string roleName)
        {
            var role = new ApplicationRole { Name = roleName };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create role");
            }
            return role;
        }

        // 
        public async Task<AspNetRoles> GetRoleById(string id)
        {
            return await _context.AspNetRoles.FirstOrDefaultAsync(r => r.Id == id);
        }


        // Kthen listen e te gjitha roleve ne Databaze
        public async Task<List<AspNetRoles>> GetRoles() 
        {
            return await _context.AspNetRoles.ToListAsync();
        }

        public async Task<IdentityResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                throw new ApplicationException("Role not found");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Error deleting role: " + result.Errors.First().Description);
            }

            return result;
        }
    }
}
