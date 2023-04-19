using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CattleMgm.Controllers
{
    [AllowAnonymous]
    public class RolesController : BaseController
    {
        public RolesController(ApplicationDbContext context, praktikadbContext db, UserManager<ApplicationUser> userManager) : base(context, db, userManager)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
