using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Models;
using CattleMgm.ViewModels.Home;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CattleMilkBarChart()
        {
            var cattleMilk = (from cm in _db.CattleMilk
                              //group cm by new { cm.Created.Date.DayOfWeek } into g
                              select new
                              {
                                  Date = cm.Created,
                                  Liters = cm.LitersCollected
                              }).ToList();

            var cattleMilkGrouped = (from c in cattleMilk
                                    group c by new { c.Date.DayOfWeek } into g
                                    select new { 
                                        Day = g.FirstOrDefault().Date.DayOfWeek,
                                        DayInt = (int)g.FirstOrDefault().Date.DayOfWeek,
                                        Liters = g.Sum(q => q.Liters)
                                    }).ToList();

            cattleMilkGrouped = cattleMilkGrouped.OrderBy(q => q.Day).ToList();

            var jsonModel = new BarChart();

            for (int i = 1; i <= 7; i++)
            {
                //int j = i + 1;

                var item = cattleMilkGrouped.Where(q => q.DayInt == i).FirstOrDefault();
                if(item == null)
                {
                    jsonModel.Data[i-1] = 0; 
                }
                else
                {
                    jsonModel.Data[i-1] = item.Liters;
                }

            }
            var sundayMilk = cattleMilkGrouped.Where(q => q.DayInt == 0).FirstOrDefault();
            if (sundayMilk == null)
            {
                jsonModel.Data[6] = 0;
            }
            else
            {
                jsonModel.Data[6] = sundayMilk.Liters;
            }

            jsonModel.Days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            return Json(jsonModel);
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

        public IActionResult Charts()
        {

            return View("chartjs");
        }


    }
}