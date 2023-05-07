using CattleMgm.Models;
using CattleMgmApi.Data.Entities;
using CattleMgmApi.Dtos.Roles;
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore; 

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
        public async Task<ApplicationRole> GetRoleById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }



        // Kthen listen e te gjitha roleve ne Databaze
        public async Task<List<AspNetRoles>> GetRoles() 
        {
            return await _context.AspNetRoles.ToListAsync();
        }

        public async Task<IdentityResult> DeleteRole(string roleId)
        {
            // Gjen rolin ne Databaze duke perdorur ID.
            var role = await _roleManager.FindByIdAsync(roleId);

            // Ne qofte se roli nuk gjindet, dergon nje exception.
            if (role == null)
            {
                throw new ApplicationException("Role not found");
            }

            // Fshin rolin nga databaza.
            var result = await _roleManager.DeleteAsync(role);

            // Ne qofte se haset ndonje error gjate procesit, dergon nje exception.
            if (!result.Succeeded)
            {
                throw new ApplicationException("Error deleting role: " + result.Errors.First().Description);
            }

            // Kthen rezultatet e operacionit delete.
            return result;
        }


        // Edit
        public async Task<bool> UpdateRole(ApplicationRole role, RolesEditDto model)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            // update the role properties with the values from the model
            role.Name = model.Name;

            // update the role in the database
            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded;
        }


        public bool RoleExists(int id)
        {
            return _context.AspNetRoles.Any(e => e.Id == id.ToString());
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
