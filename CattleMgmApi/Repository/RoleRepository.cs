using CattleMgm.Models;
using CattleMgmApi.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace CattleMgmApi.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private RoleManager<ApplicationRole> _roleManager;
        private PraktikadbContext _context;


        public RoleRepository(PraktikadbContext context)

        {
            _context = context;
        }
        public async Task<List<AspNetRoles>> GetRoles()
        {
            return  await _context.AspNetRoles.ToListAsync();
        }
    }
}
