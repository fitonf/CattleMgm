using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.Repository.CattleTemperature;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.CattleTemperature;
using CattleMgm.ViewModels.Farm;
using CattleMgm.ViewModels.Menu;
using CattleMgm.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;


namespace CattleMgm.Controllers
{
    
    public class TemperatureController : BaseController
    {
        public ICattleTempRepository _cattleTempRepository;
        public TemperatureController(ApplicationDbContext context, praktikadbContext db,
            UserManager<ApplicationUser> userManager , ICattleTempRepository CattleTempRepository): base(context, db, userManager)
        {

            _cattleTempRepository = CattleTempRepository;


        }

        public async Task<IActionResult> Index()
        {
            var temp = await _db.CattleTemperature.Include(q=> q.Cattle).ToListAsync();

            

            //var username = _db.AspNetUsers.Select(x => new { Id = x.Id, Name = x.FirstName }).ToList();
            //ViewBag.UserName = username;

         


            foreach (var item in temp) {

                model.Add(new CattleTempViewModel
                {
                    Id = item.Id,
                    CattleName= item.Cattle.Name,
                    CattleId = item.CattleId,
                    Temperature = item.Temperature,
                    DateMeasured = item.DateMeasured,
                    CreatedBy = item.CreatedBy
                   // CreatedBy = item.User.FirstName + " "+item.User.LastName,
                    //CreatedBy = item.Cattle.Farm.Farmer.FirstName
                });
            }      
            return View(model);
        }

       List<CattleTempViewModel> model = new List<CattleTempViewModel>();


        [HttpGet]
        public IActionResult Create()
        {
            var tempe = _db.Cattle.ToList();
            ViewBag.Cattle = new SelectList(tempe, "Id", "Name");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CattleTempCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ka ndodhur nje gabim. Plotesoni te dhenat obligative");
                return View(model);
            }

            CattleTemperature temp = new CattleTemperature();
            temp.CattleId = model.CattleId;
            temp.Temperature = model.Temperature;
            temp.DateMeasured = DateTime.Now;
            temp.CreatedBy = user.Id;

            await _db.CattleTemperature.AddAsync(temp);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        //Http-te e editimit
        [HttpGet]

        public IActionResult _Edit(string ide)
        {

            if (ide == null)
            {
                return BadRequest();
            }


            int id = AesCrypto.Decrypt<int>(ide);
            var temp = _db.CattleTemperature.Find(id);
            var cattles = _db.Cattle.Select(x => new { Id = x.Id, Name = x.Name }).ToList();
            ViewBag.Cattles = new SelectList(cattles, "Id", "Name");

            if (temp == null)
            {
                return NotFound();
            }

            CattleTempEditViewModel editViewModel = new CattleTempEditViewModel
            {
                Id=temp.Id,
                CattleId = temp.CattleId,
                Temperature = temp.Temperature


            };

            return PartialView(editViewModel);
        }


        [HttpPost]
        public IActionResult _Edit(CattleTempEditViewModel model)
        {

            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Temperatura eshte modifikuar me sukses", Title = "Sukses" };

            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
                return Json(error);
            }

           
            var temp = _db.CattleTemperature.Find(model.Id);

            if (temp == null)
            {
                return NotFound();
            }
           
            temp.Temperature = model.Temperature;


            _db.Update(temp);

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


            var temp = _db.CattleTemperature.Find(Id);
            if (temp != null)
            {
                var result = _db.CattleTemperature.Remove(temp);

                await _db.SaveChangesAsync();

            }

            return Json("success");

        }



    }
}
