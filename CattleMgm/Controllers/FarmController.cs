using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Models;
using CattleMgm.Repository.Farm;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Farm;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Controllers
{
    public class FarmController : BaseController
    {
        public IFarmRepository _farmRepository;
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

            List <FarmViewModel> model = new List<FarmViewModel>();

            foreach (var item in farms)
            {
                model.Add(new FarmViewModel
                {
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
                                .Select(q=> new {q.Id, FullName = q.FirstName + " " + q.LastName})
                                .ToListAsync();
            ViewBag.Farmers = new SelectList(farmers, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FarmCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Ka ndodhur nje gabim. Plotesoni te dhenat obligative");
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
    }
}
