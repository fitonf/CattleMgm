using CattleMgm.Models;
using CattleMgmApi.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace CattleMgmApi.Repository
{
    public class RoleRepository : IRolesRepository
    {
        private RoleManager<ApplicationRole> _roleManager;
        private PraktikadbContext _context;


        public RoleRepository(RoleManager<ApplicationRole> roleManager, PraktikadbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<string> CreateRole(string roleName)
        {
            var role = new ApplicationRole { Name = roleName };
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Error creating role: " + result.Errors.First().Description);
            }

            return role.Id;
        }


        public Task<AspNetRoles> GetRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AspNetRoles>> GetRoles()
        {
            return await _context.AspNetRoles.ToListAsync();
        }
    }
}