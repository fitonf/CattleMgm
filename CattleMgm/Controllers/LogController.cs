using CattleMgm.Data.Entities;
using CattleMgm.Data;
using CattleMgm.Models;
using CattleMgm.Repository.Cattles;
using CattleMgm.Repository.Farm;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CattleMgm.Helpers;
using CattleMgm.ViewModels.Cattle;
using CattleMgm.Repository.Log;
using CattleMgm.ViewModels.Log;
using CattleMgm.ViewModels.CattleTemperature;
using CattleMgm.ViewModels.Humidity;
using CattleMgm.ViewModels.Position;
using Microsoft.EntityFrameworkCore;
using CattleMgm.Helpers.Security;
using CattleMgm.ViewModels.User;

namespace CattleMgm.Controllers
{
    public class LogController : BaseController
    {
        private ILogRepository _logRepository;
        public LogController(ApplicationDbContext context, praktikadbContext db,
            UserManager<ApplicationUser> userManager,
                                ILogRepository logRepository) : base(context, db, userManager)
        {
            _logRepository = logRepository;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Lista e erroreve";

            //var lista = _logRepository.GetLogs();

            //List<LogViewModel> listaViewModel = new List<LogViewModel>();

            //foreach (var log in lista)
            //{
            //    listaViewModel.Add(new LogViewModel
            //    {
            //        Id = (int)log.LogId,
            //        UserId = log.UserId,
            //        Controller = log.Controller,
            //        Action = log.Action,
            //        HttpMethod = log.HttpMethod,
            //        Date=log.InsertedDate.ToShortDateString(),
            //        Url = log.Url
            //    });
            //}

            //listaViewModel = listaViewModel.OrderByDescending(q => q.Id).ToList();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var log = await _db.Log.Where(q => q.LogId == id).FirstOrDefaultAsync();



            if (log == null)
            {
                return NotFound();
            }

            int index = log.Url.LastIndexOf('/');
            int index1 = log.Exception.IndexOf(',')-1;
            LogDetailsViewModel model = new LogDetailsViewModel();

            model.Id = (int)log.LogId;
            model.UserId = log.UserId;
            model.Controller = log.Controller;
            model.Action = log.Action;
            model.HttpMethod = log.HttpMethod;
            model.Url = log.Url.Substring(0,index);
            model.Exception = log.Exception.Substring(1,index1);


            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> _Index(SearchLogs search)
        {
            DateTime dp;

            List<LogViewModel> model = (from item in _db.Log
                                         where
                                         ((item.UserId == search.UserId || search.UserId == null) &&
                                         (item.Controller == search.Controller || search.Controller == null))
                                         select new LogViewModel
                                         {
                                             Id = (int)item.LogId,
                                             UserId = item.UserId,
                                             Controller = item.Controller,
                                             Action=item.Action,
                                             HttpMethod=item.HttpMethod,
                                             Date=item.InsertedDate
                                         }).ToList();

            if (search.DateMeasured != null)
            {
                dp = DateTime.Parse(search.DateMeasured).Date;

                model = model.Where(q => q.Date.Date == dp).ToList();
            }

            return Json(model);
        }
    }
}
