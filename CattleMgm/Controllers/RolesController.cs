using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CattleMgm.ViewModels.Roles;

namespace CattleMgm.Controllers
{
    [Authorize]
    public class RolesController : BaseController
    {
        public readonly RoleManager<ApplicationRole> roleManager;


        public RolesController(ApplicationDbContext context, praktikadbContext db,
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : base(context, db, userManager)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            List<RolesViewModel> model = new List<RolesViewModel>();


            model = (from item in roles
                     select new RolesViewModel
                     {
                         Id = item.Id,
                         Name = item.Name
                     }).ToList();                

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RolesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.Name
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync((ApplicationRole)identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Roles");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);

        }

    }
}
