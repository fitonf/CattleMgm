using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.Models.Session;
using CattleMgm.Repository.CattleBloodPressures;
using CattleMgm.ViewModels.CattleBloodPressure;
using CattleMgm.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Net;

namespace CattleMgm.Controllers
{
    public class CattleBloodPressureController : BaseController
    {
        ICattleBloodPressureRepository _cattleBloodPressure;
        public CattleBloodPressureController(ApplicationDbContext context, praktikadbContext db, UserManager<ApplicationUser> userManager,
            ICattleBloodPressureRepository cattleBloodPressure ) : base(context, db, userManager)
        {
            _cattleBloodPressure = cattleBloodPressure;
        }

            public IActionResult Index()
             {
                var lista = _cattleBloodPressure.GetCattleBloodPressures();
               
                //var lista  = _db.CattleBloodPressure.ToList();
                List<CattleBloodPressureViewModel> model = new List<CattleBloodPressureViewModel>();

            model = (from item in lista select new CattleBloodPressureViewModel
            {
                Id = item.Id,
                CattleName = item.Cattle.Name,
                PressureFrom = item.SystolicValue,
                PressureTo = item.DiastolicValue,
                DateMeasured = item.DateMeasured.ToShortDateString(),                

            }).ToList();

                return View(model);
            }

        [HttpGet]
        public IActionResult Create()
        {
            var cattle = _db.Cattle.ToList();
            ViewBag.CattleId = new SelectList(cattle, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CattleBloodPressureCreateViewModel model){
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Plotesoni fushat obligative");
                
                var cattle = _db.Cattle.ToList();
                ViewBag.CattleId = new SelectList(cattle, "Id", "Name");

                return View(model);
            }      

            CattleBloodPressure newCattle = Activator.CreateInstance<CattleBloodPressure>(); //new CattleBloodPressure();
            newCattle.CattleId = model.CattleId;
            newCattle.SystolicValue = model.PressureFrom;
            newCattle.DiastolicValue = model.PressureTo;
            newCattle.DateMeasured = DateTime.Now;
            newCattle.CreatedBy = user.Id;
 
            _db.CattleBloodPressure.Add(newCattle);
            await _db.SaveChangesAsync();

            //ka ndodh nje gabim
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            //var did = AesCrypto.Decrypt<int>(id);
            if (id == null)
            {
                ModelState.AddModelError("", "Id eshte e zbrazet");
                return NotFound();
            }

            var cattle = await _db.CattleBloodPressure.FindAsync(id);

            if (cattle == null)
            {
                ModelState.AddModelError("", "Kjo Cattle nuk ekziston");
                return NotFound();
            }

            var cattles = _db.Cattle.ToList();
            ViewBag.CattleId = new SelectList(cattles, "Id", "Name");

            CattleBloodPressureEditViewModel model = new CattleBloodPressureEditViewModel();
            model.Id = cattle.Id;
            model.CattleId = cattle.CattleId;
            model.PressureFrom = cattle.SystolicValue;
            model.PressureTo = cattle.DiastolicValue;
            model.DateMeasured = cattle.DateMeasured.ToShortDateString();    

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(CattleBloodPressureEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Plotesoni fushat obligative");
                return View(model);
            }

            var cattle = await _db.CattleBloodPressure.FindAsync(model.Id);

            if (cattle == null)
            {
                ModelState.AddModelError("", "Kjo Cattle nuk ekziston");
                return View(model);
            }
        
            cattle.CattleId = model.CattleId;
            cattle.SystolicValue = model.PressureFrom;
            cattle.DiastolicValue = model.PressureTo;
            cattle.DateMeasured = Convert.ToDateTime(model.DateMeasured);           

            try
            {
                _db.CattleBloodPressure.Update(cattle);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "CattleBloodPressure");

            }
            catch
            {
                ModelState.AddModelError("", "Ka ndodhur nje Gabim");
                return View(model);
            }

        }


        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id) 
        {
            if (id == 0)
            {
                //ModelState.AddModelError("", "");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Kjo id nuk ekziston");
            }
            
            var cattle = await _db.CattleBloodPressure.FindAsync(id);
            
            if (cattle is not null)
            {
                _db.CattleBloodPressure.Remove(cattle);
                await _db.SaveChangesAsync();                
            }

            return Json("success");

        }
        [HttpPost]
        public async Task<JsonResult> OpenIndexReport()
        {

            var cattle = _db.Cattle.ToList();//
            var farm = _db.Farm.ToList();
            var breed = _db.Breed.ToList();
            var municipality = _db.CattleBloodPressure.ToList();




            var query = cattle


               .Select(q => new CattleReportModel
               {
                   Id = q.Id,
                   Name = q.Name,
                   Weight = q.Weight,
                   BreedId = q.Breed.Name,
                   BrithDate = q.BirthDate,
                   Gender = q.Gender == 1 ? "Mashkull" : "Femer",
                   FarmId = q.Farm.Name,
                   MunicipalityId = q.Municipality.Emri
               }).ToList();


            HttpContext.Session.SetString("Path", "Reports\\CattleBloodPressureRaport.rdl");
            HttpContext.Session.Set("queryresult", query);


            return Json(true);
        }
    }
    }

