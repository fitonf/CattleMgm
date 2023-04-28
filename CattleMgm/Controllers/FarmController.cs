using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.Repository.Farm;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Farm;
using CattleMgm.ViewModels.Menu;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CattleMgm.Controllers
{
    public class FarmController : BaseController
    {
        public IFarmRepository _farmRepository;
        private object farm;
        private object model;

        public FarmController(ApplicationDbContext context,
            praktikadbContext db,
            UserManager<ApplicationUser> userManager,
            IFarmRepository farmRepository) : base(context, db, userManager)
        {
            _farmRepository = farmRepository;
        }


        public async Task<IActionResult> Index()
        {
            var farms = new List<Farm>();
            var farmerId = await _db.Farmer.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (!User.IsInRole("Administrator"))
            {
                farms = await _farmRepository.GetAllFarms(farmerId.Id);
            }
            else
            {
                farms = await _farmRepository.GetAllFarms();
            }

            List<FarmViewModel> model = new List<FarmViewModel>();

            foreach (var item in farms)
            {
                model.Add(new FarmViewModel
                {
                    Id = item.Id,
                    FarmerName = $"{item.Farmer.FirstName} {item.Farmer.LastName}",
                    FarmName = item.Name,
                    Place = item.Place,
                    Address = item.Address,
                    Active = item.Active

                });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var farmers = await _db.Farmer.Where(x => x.UserId != null)
                                .Select(q => new { q.Id, FullName = q.FirstName + " " + q.LastName })
                                .ToListAsync();
            ViewBag.Farmers = new SelectList(farmers, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FarmCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ka ndodhur nje gabim. Plotesoni te dhenat obligative");
                return View(model);
            }

            Farm farm = new Farm();
            farm.FarmerId = model.FarmerId;
            farm.Name = model.FarmName;
            farm.Place = model.Place;
            farm.Address = model.Address;
            farm.Created = DateTime.Now;
            farm.CreatedBy = user.Id;

            await _db.Farm.AddAsync(farm);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            if (id == null)
            {
                return BadRequest();
            }

            //int did = AesCrypto.Decrypt<int>(ide);

            var item = _db.Farm.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            FarmEditViewModel editViewModel = new FarmEditViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Place = item.Place,
                Address = item.Address,
                Active = item.Active,
            };

            return View(editViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditAsync(FarmEditViewModel model)
        {

            //ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Ferma eshte modifikuar me sukses", Title = "Sukses" };

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Plotesoni fushat obligative");
                return View(model);
              //  error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
               // return Json(error);
            }

            var item = _db.Farm.Find(model.Id);

            if (item == null)
            {
                ModelState.AddModelError("", "Ferma nuk ekziston");
                return View(model);
            }

            item.Id = model.Id;
            item.Name = model.Name;
            item.Place = model.Place;
            item.Address = model.Address;
            item.Active = model.Active;


            _db.Update(item);

            _db.SaveChanges();

            return RedirectToAction("Index", "Farm");
        }
    
    }
}
