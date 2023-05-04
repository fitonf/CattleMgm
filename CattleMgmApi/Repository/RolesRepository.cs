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

        public async Task<string> CreateRole(string roleName)
        {
            // Krijon nje ApplicationRole object me emrin e specifikuar.
            var role = new ApplicationRole { Name = roleName };
            // Perdor RoleManager per te krijuar nje rol te ri.
            var result = await _roleManager.CreateAsync(role);

            // Ne qofte se procesi ka qene i pa suksesshem, dergon nje exception.
            if (!result.Succeeded) 
            {
                throw new ApplicationException("Error creating role: " + result.Errors.First().Description);
            }

            // Nqs ka sukses, kthen ID rolin.
            return role.Id; 
        }

        // Jo implementuar kompletisht
        public Task<AspNetRoles> GetRoleById(int id) 
        {
            throw new NotImplementedException();
        }

        // Kthen listen e te gjitha roleve ne Databaze
        public async Task<List<AspNetRoles>> GetRoles() 
        {
            return await _context.AspNetRoles.ToListAsync();
        }
    }
}
