using CattleMgm.Models;
using CattleMgm.Models.Menu;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ListOfMenus>().HasNoKey();
            builder.Entity<ListOfMenusAccess>().HasNoKey();
            //builder.Entity<ProcedureModel>().HasNoKey();
            base.OnModelCreating(builder);
        }
    }
}