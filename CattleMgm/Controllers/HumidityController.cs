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
using CattleMgm.Models;
using CattleMgm.ViewModels.User;



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
            var cattle = await _db.Cattle.Select(q => new { q.Id, FullName = q.Name })
                                .ToListAsync();
        ViewBag.Cattles = new SelectList(cattle, "Id", "FullName");
            return View();
            //var hum = await _db.CattleHumidity.Include(q => q.Cattle).ToListAsync();
            //List<HumidityViewModel> model = new List<HumidityViewModel>();

            //foreach (var item in hum)
            //{

            //    model.Add(new HumidityViewModel
            //    {
            //        Id = item.Id,
            //        CattleName = item.Cattle.Name,
            //        CattleId = item.CattleId,
            //        Humidity = item.Humidity,
            //        DateMeasured = item.DateMeasured,
            //        CreatedBy = user == null ? "" : user.FirstName + " " + user.LastName
            //    });
            //}
            //model  = model.OrderByDescending(q => q.Id).ToList();
         
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
        public async Task<IActionResult> Delete(string id)
        {

            var did = AesCrypto.Decrypt<int>(id);
            if (id == null)
            {
                return BadRequest();
            }


            //var cattle = _db.CattleHumidity.Find(Id);
            var cattle = _db.CattleHumidity.Find(did);
            if (cattle != null)
            {
                var result = _db.CattleHumidity.Remove(cattle);

                await _db.SaveChangesAsync();

            }

            return Json("success");

        }

        [HttpPost]
        public async Task<IActionResult> _Index(SearchHumidity search)
        {
            List<HumidityViewModel> model = (from item in _db.CattleHumidity.Include(x => x.Cattle)

                                             where
                                          ((item.CattleId == search.CattleId || search.CattleId== null) &&
                                          (item.Humidity == search.Humidity || search.Humidity == null))

                                          select new HumidityViewModel
                                          {
                                              Id = AesCrypto.Enkrypt(item.Id),
                                              CattleName = item.Cattle.Name,
                                              Humidity = item.Humidity,
                                              DateMeasured = item.DateMeasured,
                                              CreatedBy = user.Id,

        }).ToList();




            return Json(model);
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