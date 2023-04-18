using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CattleMgm.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly praktikadbContext _db;
        public readonly UserManager<ApplicationUser> _userManager;

        protected ApplicationUser user = new ApplicationUser();

        public BaseController(ApplicationDbContext context, 
            praktikadbContext db, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _db = db;
            _userManager = userManager;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            user = await _userManager.GetUserAsync(context.HttpContext.User);

            ViewData["User"] = new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Language = user.Language,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber
            };

            await next();
        }

    }
}
