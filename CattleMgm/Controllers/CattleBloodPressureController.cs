using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.Repository.CattleBloodPressures;
using CattleMgm.ViewModels.CattleBloodPressure;
using CattleMgm.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CattleMgm.Controllers
{
    public class CattleBloodPressureController : BaseController
    {

        ICattleBloodPressure _cattleBloodPressure;
        public CattleBloodPressureController(ApplicationDbContext context, praktikadbContext db, UserManager<ApplicationUser> userManager,
            ICattleBloodPressure cattleBloodPressure) : base(context, db, userManager)
        {
            _cattleBloodPressure = cattleBloodPressure;
        }

        public IActionResult Index()
        {
           // var cattle = _db.CattleBloodPressure.ToList();
           
            var lista = _cattleBloodPressure.GetCattleBloodPressure();

            List<CattleBloodPressureViewModel> model = new List<CattleBloodPressureViewModel>();

            model = (from item in lista
                     select new CattleBloodPressureViewModel
                     {
                         CattleName = item.Cattle.Name,
                         PresureFrom = item.SystolicValue,
                         PresureTo = item.DiastolicValue,
                         DateMeasured = item.DateMeasured.ToShortDateString(),      
                         Id = item.Cattle.Id

                     }).ToList();

            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var Cattles = _db.Cattle.ToList();

            ViewBag.CattleId = new SelectList(Cattles, "Id", "Name");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(CattleBloodPressureCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Plotesoni fushat obligative");

                var Cattles = _db.Cattle.ToList();

                ViewBag.CattleId = new SelectList(Cattles, "Id", "Name");

                return View(model);
            }

            CattleBloodPressure newCattle = Activator.CreateInstance<CattleBloodPressure>(); //new CattleBloodPressure();
            newCattle.CattleId = model.CattleId;
            newCattle.SystolicValue = model.PresureFrom;
            newCattle.DiastolicValue = model.PresureTo;
            newCattle.DateMeasured = DateTime.Now;
            newCattle.CreatedBy = user.Id;

            _db.CattleBloodPressure.Add(newCattle);
            await _db.SaveChangesAsync();

            //ka ndodh nje gabim
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null || id ==0)
            {
                return NotFound();
            }
            var Cattles = _db.Cattle.ToList();
            
            var cattle = _cattleBloodPressure.GetCattleBloodPressureById((int)id);

            var cattleBloodPressure = new CattleBloodPressureEditViewModel
            {
                PresureFrom = cattle.SystolicValue,
                PresureTo = cattle.DiastolicValue,
                Id = cattle.Cattle.Id,
            };

            ViewBag.CattleId = new SelectList(Cattles, "Id", "Name");

            if (cattleBloodPressure == null) 
            {
                return NotFound(); 
            }

            return View(cattleBloodPressure);
        }


        [HttpPost]
        public async Task<IActionResult> EditAsync(CattleBloodPressureEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Plotesoni fushat obligative");

                var Cattles = _db.Cattle.ToList();

                ViewBag.CattleId = new SelectList(Cattles, "Id", "Name");

                return View(model);
            }

            CattleBloodPressure newCattle = Activator.CreateInstance<CattleBloodPressure>(); //new CattleBloodPressure();
            newCattle.CattleId = model.CattleId;
            newCattle.SystolicValue = model.PresureFrom;
            newCattle.DiastolicValue = model.PresureTo;
            newCattle.DateMeasured = DateTime.Now;
            newCattle.CreatedBy = user.Id;

            _db.CattleBloodPressure.Add(newCattle);
            await _db.SaveChangesAsync();

            //ka ndodh nje gabim
            return RedirectToAction("Index");
        }
    }
    }

