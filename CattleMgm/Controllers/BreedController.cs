using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.Models.Session;
using CattleMgm.Repository.Cattles;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Breed;
using CattleMgm.ViewModels.Menu;
using CattleMgm.ViewModels.Municipality;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CattleMgm.Controllers
{
    public class BreedController : BaseController
    {
        public ICattleRepository _cattleRepository;
        public BreedController(ApplicationDbContext context
            , praktikadbContext db
            , UserManager<ApplicationUser> userManager
            , ICattleRepository cattleRepository
            ) : base(context, db, userManager)
        {
            _cattleRepository = cattleRepository;
        }
        public IActionResult Index()
        {
            var breeds = _db.Breed.Select(q => new BreedViewModel
            {
                Id = q.Id,
                Name = q.Name,
                Type = q.Type
               
            }).ToList();

            return View(breeds);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(BreedCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ka ndodhur nje gabim. Plotesoni te dhenat obligative");
                return View(model);
            }
            var existingBreed = await _db.Breed.FirstOrDefaultAsync(b => b.Name == model.Name || b.Type == model.Type);

            if (existingBreed != null)
            {
                if (existingBreed.Name == model.Name && existingBreed.Type == model.Type)
                {
                    ModelState.AddModelError("", "Nje kafshe me kete emer dhe tip ekziston tashme");
                }
                else if (existingBreed.Name == model.Name)
                {
                    ModelState.AddModelError("", "Nje kafshe me kete emer ekziston tashme");
                }
                else
                {
                    ModelState.AddModelError("", "Nje kafshe me kete tip ekziston tashme");
                }

                return View(model);
            }

            Breed breed = new Breed();

            breed.Name = model.Name;
            breed.Type = model.Type;
            breed.Created = DateTime.Now;
            breed.CreatedBy = user.Id;

            await _db.Breed.AddAsync(breed);
            await _db.SaveChangesAsync();


            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Edit(string? ide)
        {

            if (ide == null)
            {
                return BadRequest();
            }

            int did = AesCrypto.Decrypt<int>(ide);

            var breed = _db.Breed.Find(did);

            if (breed == null)
            {
                return NotFound();
            }

            BreedEditViewModel editViewModel = new BreedEditViewModel
            {
                Id = did,
                Name = breed.Name,
               Type =breed.Type
            };

            return PartialView(editViewModel);
        }

        [HttpPost]
        public IActionResult Edit(BreedEditViewModel model)
        {

            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Breed eshte modifikuar me sukses", Title = "Sukses" };

            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
                return Json(error);
            }

            var breed = _db.Breed.Find(model.Id);

            if (breed == null)
            {
                return NotFound();
            }
            var existingBreed = _db.Breed.FirstOrDefault(b => b.Id != model.Id && (b.Name == model.Name || b.Type == model.Type));

            if (existingBreed != null)
            {
                if (existingBreed.Name == model.Name && existingBreed.Type == model.Type)
                {
                    ModelState.AddModelError("", "Nje kafshe me kete emer dhe tip ekziston tashme");
                }
                else if (existingBreed.Name == model.Name)
                {
                    ModelState.AddModelError("", "Nje kafshe me kete emer ekziston tashme");
                }
                else
                {
                    ModelState.AddModelError("", "Nje kafshe me kete tip ekziston tashme");
                }

                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Nje kafshe me kete emer ose tip ekziston tashme", Title = "Lajmerim" };
                return Json(error);
            }


            breed.Name = model.Name;
            breed.Type = model.Type;
            breed.LastUpdated = DateTime.Now;
            breed.LastUpdatedBy = user.Id;



            _db.Update(breed);

            _db.SaveChanges();

            return Json(error);
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

            var breed = await _db.Breed.FindAsync(id);

            if (breed is not null)
            {
                _db.Breed.Remove(breed);
                await _db.SaveChangesAsync();
            }

            return Json("success");

        }

        [HttpPost]
        public async Task<JsonResult> OpenIndexReport()
        {
            var breed = _db.Breed.ToList();
            var query = breed
               .Select(q => new BreedReportModel
               {
                   Id = q.Id,
                   Name = q.Name,
                   Type = q.Type
               }).ToList();


            HttpContext.Session.SetString("Path", "Reports\\BreedReport.rdl");
            HttpContext.Session.Set("queryresult", query);


            return Json(true);
        }
    }

  
}
