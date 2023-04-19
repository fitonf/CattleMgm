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
            this.roleManager = roleManager;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolesCreateViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                //IdentityRole identityRole = new IdentityRole
                ApplicationRole identityRole = new ApplicationRole

                {
                    Name = model.Name,
                    NormalizedName = model.Name.ToUpper()
                };

                 IdentityResult result = await roleManager.CreateAsync(identityRole);

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
