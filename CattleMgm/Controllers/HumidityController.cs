using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.Repository.Humidity;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Humidity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CattleMgm.Models.Session;
using System.Net;


namespace CattleMgm.Controllers
{
    public class HumidityController : BaseController
    {
        public IHumidityRepository _humidityRepository;
        public HumidityController(ApplicationDbContext context, praktikadbContext db,
            UserManager<ApplicationUser> userManager, IHumidityRepository HumidityRepository) : base(context, db, userManager)
        {
            _humidityRepository = HumidityRepository;
        }

        public async Task<IActionResult> Index()
        {
            var hum = await _db.CattleHumidity.Include(q => q.Cattle).ToListAsync();
            List<HumidityViewModel> model = new List<HumidityViewModel>();

            foreach (var item in hum)
            {

                model.Add(new HumidityViewModel
                {
                    Id = item.Id,
                    CattleName = item.Cattle.Name,
                    CattleId = item.CattleId,
                    Humidity = item.Humidity,
                    DateMeasured = item.DateMeasured,
                    CreatedBy = user == null ? "" : user.FirstName + " " + user.LastName
                });
            }
            model  = model.OrderByDescending(q => q.Id).ToList();
            return View(model);
        }

       


        [HttpGet]
        public IActionResult Create()
        {
            var humid = _db.Cattle.ToList();
            ViewBag.Cattle = new SelectList(humid, "Id", "Name");

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create(HumidityCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ka ndodhur nje gabim. Plotesoni te dhenat obligative");
                return View(model);
            }

            CattleHumidity hum = new CattleHumidity();
            hum.CattleId = model.CattleId;
            hum.Humidity = model.Humidity;
            hum.DateMeasured = DateTime.Now;
            hum.CreatedBy = user.Id;

            await _db.CattleHumidity.AddAsync(hum);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> IsHumidityAvailable(int CattleId, double Humidity)
        {
            // Merr rekordin e fundit per Gjedhen e zgjidhur.
            var latestHumidity = await _db.CattleHumidity
                .Where(h => h.CattleId == CattleId)
                .OrderByDescending(h => h.DateMeasured)
                .FirstOrDefaultAsync();

            // Ne qofte nuk ekziston ndonje rekord, kthen JSON true e cila lejon perdoruesin te vazhdoj.
            if (latestHumidity == null)
            {
                return Json(true);
            }

            // Deklaron variablen e kohes se matjes se fundit.
            var timeElapsed = DateTime.Now - latestHumidity.DateMeasured;

            // Ne qofte se me pak se 3 ore kane kaluar, atehere kthen JSON Error message e cila tregon edhe kur eshte bere matja e fundit.
            if ((DateTime.Now - latestHumidity.DateMeasured).TotalHours <= 3)
            {
                return Json($"Lagështia për këtë gjedh është regjistruar më parë {Math.Round(timeElapsed.TotalMinutes, 2)} minuta më parë.");
            }

            // Perndryshe, kthen JSON true.
            return Json(true);
        }


        [HttpGet]

        public IActionResult Edit(string ide)
        {

            if (ide == null)
            {
                return BadRequest();
            }


            int id = AesCrypto.Decrypt<int>(ide);
            var hum = _db.CattleHumidity.Find(id);
            var cattles = _db.Cattle.Select(x => new { Id = x.Id, Name = x.Name }).ToList();
            ViewBag.Cattles = new SelectList(cattles, "Id", "Name");

            if (hum == null)
            {
                return NotFound();
            }

            HumidityEditViewModel editViewModel = new HumidityEditViewModel
            {
                Id = hum.Id,
                CattleId = hum.CattleId,
                Humidity = hum.Humidity


            };

            return PartialView(editViewModel);
        }


        [HttpPost]
        public IActionResult Edit(HumidityEditViewModel model)
        {

            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Lageshtia u ndryshua me sukses", Title = "Sukses" };

            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
                return Json(error);
            }


            var hum = _db.CattleHumidity.Find(model.Id);

            if (hum == null)
            {
                return NotFound();
            }

            hum.Humidity = model.Humidity;


            _db.Update(hum);

            _db.SaveChanges();

            return Json(error);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {

            if (Id == null)
            {
                return BadRequest();
            }


            var cattle = _db.CattleHumidity.Find(Id);
            if (cattle != null)
            {
                var result = _db.CattleHumidity.Remove(cattle);

                await _db.SaveChangesAsync();

            }

            return Json("success");

        }
        [HttpPost]
        public async Task<JsonResult> OpenIndexReport()
        {

            var humidity = _db.CattleHumidity.ToList();
            var cattles = _db.Cattle.ToList();
            var query = humidity


               .Select(q => new HumidityReportModel
               {

                   Id = q.Id,
                   CattleId = q.Cattle.Name,
                   Humidity = q.Humidity,
                   DateMeasured = q.DateMeasured,
                   CreatedBy = user == null ? " " : user.FirstName + " " + user.LastName



               }).ToList();


            HttpContext.Session.SetString("Path", "Reports\\HumidityReport.rdl");
            HttpContext.Session.Set("queryresult", query);


            return Json(true);
        }
    }
}