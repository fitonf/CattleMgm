using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.Repository.Cattles;
using CattleMgm.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CattleMgm.Controllers
{
    [Authorize(policy: "u:1")]
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
                         Id = AesCrypto.EncryptString(item.Id),
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

            ApplicationUser newUser = Activator.CreateInstance<ApplicationUser>(); //new ApplicationUser();
            newUser.FirstName = model.FirstName;
            newUser.LastName = model.LastName;
            newUser.Email = model.Email;
            newUser.UserName = model.UserName;
            newUser.PhoneNumber = model.PhoneNumber;
            //newUser.RoleId = model.RoleId;

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if(result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(newUser, role.Name);//e ben insertimin e rolit ne databaze
                return RedirectToAction("Index", "User");
            }

            //ka ndodh nje gabim
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(string id)
        {
            var did = AesCrypto.Decrypt<string>(id);
            if (String.IsNullOrEmpty(did))
            {
                ModelState.AddModelError("", "Id eshte e zbrazet");
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(did);

            if(user ==null)
            {
                ModelState.AddModelError("", "Ky user nuk ekziston");
                return NotFound();
            }

            var roles = _db.AspNetRoles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            UserEditViewModel model = new UserEditViewModel();
            model.Id = user.Id;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.UserName = user.UserName;

            var roleName = await _userManager.GetRolesAsync(user);
            //gjetja e rolit sipas emrit
            var roleUser = await _roleManager.FindByNameAsync(roleName.First());

            model.RoleId = roleUser.Id;
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(UserEditViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Plotesoni fushat obligative");
                return View(model);
            }

            //
            var user = await _userManager.FindByIdAsync(model.Id);

            if(user ==null)
            {
                ModelState.AddModelError("", "Ky user nuk ekziston");
                return View(model);
            }

            //ndryshimi i te dhenave perveq rolit nga model ne db 
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.UserName;

            //gjetja e rolit 
            var role = await _roleManager.FindByIdAsync(user.RoleId);
            if (role == null)
            {
                ModelState.AddModelError("", "Ky rol nuk ekziston");
                return View(model);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var rolename = roles.First();
            
            var oldRole = await _userManager.RemoveFromRoleAsync(user, rolename);

            var newRole = await _roleManager.FindByIdAsync(model.RoleId);

            var roleUpdated =await  _userManager.AddToRoleAsync(user, newRole.Name);

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "User");
            }

            //ka ndodh nje gabim
            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                //ModelState.AddModelError("", "");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Kjo id nuk ekziston");
            }

            //gjetja e user-it
            var user = await _userManager.FindByIdAsync(id);
            if (user is not null)
            {
                var result = await _userManager.DeleteAsync(user);
            }

            return Json("success");

        }
    }
}
