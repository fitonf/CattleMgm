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
        
        // Konstructor e cila merr parametrat e nevojitura per te kryer metodat.
        public RoleRepository(RoleManager<ApplicationRole> roleManager, PraktikadbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        // Krijon nje rol te ri me emrin e dhene.
        public async Task<ApplicationRole> CreateRole(string roleName)
        {
            // Kthen objektin e ri te krijuar ApplicationRole.
            var role = new ApplicationRole { Name = roleName };
            var result = await _roleManager.CreateAsync(role);

            // Dergon exception ne qofte se procesi deshton.
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create role");
            }
            return role;
        }

        // Gets a role by its ID
        public async Task<ApplicationRole> GetRoleById(string id)
        {
            // Kthen objektin e nevojitur te ApplicationRole (i lidhur permes _roleManager).
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
            // Shikon ne qofte se objekti eshte null.
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            // Perditeson vetite e rolit me vlerat nga modeli.
            role.Name = model.Name;

            // Perditeson rolin ne databaze.
            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded;
        }



        public bool RoleExists(int id)
        {
            // Shikon ne qofte se roli me kete ID ekziston ne databaze.
            return _context.AspNetRoles.Any(e => e.Id == id.ToString());
        }

        public void SaveChanges()
        {
            // Ruan ne databaze ndryshimet e bera.
            _context.SaveChanges();
        }


    }
}
