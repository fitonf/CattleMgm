using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CattleMgm.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, praktikadbContext db
            , UserManager<ApplicationUser> userManager, IEmailSender emailSender) : base(context, db, userManager)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        public async Task<ViewResult> Index()
        {
            
           return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ViewModels.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}