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


        // Roles Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new RolesEditViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RolesEditViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.Name;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View();
            }
        }

        //Delete Role
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("Error");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Index");
            }

        }
    }
}
