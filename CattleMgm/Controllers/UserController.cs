using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Models;
using CattleMgm.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CattleMgm.Controllers
{
    public class UserController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager; 

        public UserController(ApplicationDbContext context, praktikadbContext db, 
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager) : base(context, db)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _db.AspNetUsers.ToList();
            List<UserViewModel> model = new List<UserViewModel>();

            //foreach method
            //foreach (var item in users)
            //{
            //    model.Add(new UserViewModel
            //    {
            //        Id = item.Id,
            //        UserName = item.UserName,
            //        Email = item.Email,
            //        FirstName = item.FirstName,
            //        LastName = item.LastName,
            //        PhoneNumber = item.PhoneNumber,
            //        isRoleConfirmed = item.IsRoleConfirmed == null ?false : item.IsRoleConfirmed.Value,
            //    });
            //}

            //linq method same as foreach method
            model = (from item in users
                     select new UserViewModel
                     {
                         Id = item.Id,
                         UserName = item.UserName,
                         Email = item.Email,
                         FirstName = item.FirstName,
                         LastName = item.LastName,
                         PhoneNumber = item.PhoneNumber,
                         isRoleConfirmed = item.IsRoleConfirmed == null ? false : item.IsRoleConfirmed.Value,
                     }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roles = _db.AspNetRoles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Plotesoni fushat obligative");
                return View(model);
            }

            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                ModelState.AddModelError("", "Ky rol nuk ekziston");
                return View(model);
            }

            ApplicationUser newUser = Activator.CreateInstance<ApplicationUser>();
            newUser.FirstName = model.FirstName;
            newUser.LastName = model.LastName;
            newUser.Email = model.Email;
            newUser.UserName = model.UserName;
            newUser.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if(result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(newUser, role.Name);
                return RedirectToAction("Index", "User");
            }

            //ka ndodh nje gabim
            return View(model);
        }
    }
}
