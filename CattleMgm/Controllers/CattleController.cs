using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.Repository.Cattles;
using CattleMgm.Repository.Farm;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Cattle;
using CattleMgm.ViewModels.CattleTemperature;
using CattleMgm.ViewModels.Humidity;
using CattleMgm.ViewModels.Municipality;
using CattleMgm.ViewModels.Position;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CattleMgm.Controllers
{
    [Authorize(Policy = "t22:1")]
    public class CattleController : BaseController
    {

        private ICattleRepository _cattleRepository;
        private IFarmRepository _farmRepository;
        public CattleController(ApplicationDbContext context, praktikadbContext db,
            UserManager<ApplicationUser> userManager,
                                ICattleRepository cattleRepository, IFarmRepository farmRepository) : base(context, db, userManager)
        {

            _cattleRepository = cattleRepository;
            _farmRepository = farmRepository;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Lista e gjedheve";

            var lista = _cattleRepository.GetCattles(); //Cattle 

            List<CattleViewModel> listaViewModel = new List<CattleViewModel>(); // 

            foreach (var cattle in lista)
            {
                listaViewModel.Add(new CattleViewModel
                {
                    Id = cattle.Id,
                    Name = cattle.Name,
                    Gender = cattle.Gender == (int)Gender.Male ? "Mashkull" : "Femer",
                    BirthDate = cattle.BirthDate.ToShortDateString(),
                    FarmName = cattle.Farm.Name,
                    FarmerName = cattle.Farm.Farmer.FirstName +
                    " " + cattle.Farm.Farmer.LastName,
                    Breed = cattle.Breed.Name,
                    UniqueIdentifier = cattle.UniqueIdentifier.ToString(),
                    Weight = cattle.Weight,
                    CreatedBy = cattle.CreatedByNavigation.FirstName + cattle.CreatedByNavigation.LastName,
                    Komuna=cattle.Municipality.Emri

                });
            } //cattleviewmodel

            listaViewModel = listaViewModel.OrderByDescending(q => q.Id).ToList();

            return View(listaViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var farms = await _farmRepository.GetAllFarms();
            var breed = await _db.Breed.Select(q => new { q.Id, q.Name }).ToListAsync();

            ViewBag.Farms = new SelectList(farms, "Id", "Name");
            ViewBag.Breed = new SelectList(breed, "Id", "Name");

            List<SelectListItem> gender = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Mashkull", Value = 1.ToString() },
                new SelectListItem() { Text = "Femer", Value = 2.ToString() }
            };

            ViewBag.Gender = new SelectList(gender, "Value", "Text");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CattleCreateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                var farms = await _farmRepository.GetAllFarms();
                var breed = await _db.Breed.Select(q => new { q.Id, q.Name }).ToListAsync();

                ViewBag.Farms = new SelectList(farms, "Id", "Name");
                ViewBag.Breed = new SelectList(breed, "Id", "Name");

                List<SelectListItem> gender = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Mashkull", Value = 1.ToString() },
                new SelectListItem() { Text = "Femer", Value = 2.ToString() }
            };

                ViewBag.Gender = new SelectList(gender, "Value", "Text");

                return View(model);
            }

            Cattle cattle = new Cattle() {
                UniqueIdentifier = Guid.NewGuid(),
                FarmId = model.FarmId,
                BreedId = model.BreedId,
                Name = model.Name,
                Gender = model.Gender,
                BirthDate = DateTime.Parse(model.BirthDate),
                CreatedBy = user.Id,
                Created = DateTime.Now,
                Weight = model.Weight,
               
                
            };

            await _db.Cattle.AddAsync(cattle);

            await _db.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var cattle = await _db.Cattle.Include(q => q.Farm)
                                         .ThenInclude(q => q.Farmer)
                                         .Include(q => q.Breed)
                                         .Include(q => q.CattleBloodPressure)
                                         .Include(q => q.CattleTemperature)
                                         .Include(q => q.CattlePosition)
                                         .Include(q => q.CattleHumidity)
                                         .Include(q=>q.Municipality)
                                         .Include(q => q.CattleMilk).Where(q => q.Id == id).FirstOrDefaultAsync();



            if(cattle == null)
            {
                return NotFound();
            }    

            CattleDetailsViewModel model = new CattleDetailsViewModel();

            model.Id = cattle.Id;
            model.UniqueIdentifier = cattle.UniqueIdentifier.ToString();
            model.Name = cattle.Name;
            model.Weight = cattle.Weight;
            model.FarmerName = cattle.Farm.Farmer.FirstName + " " + cattle.Farm.Farmer.LastName;
            model.FarmName = cattle.Farm.Name;
            model.Breed = cattle.Breed.Name;
            model.BirthDate = cattle.BirthDate.ToString("dd/MM/yyyy HH:mm:ss");
            model.MilkCollectedToday = cattle.CattleMilk.Where(x => x.Created.Date == DateTime.Now.Date && x.CattleId == model.Id).Any();
            model.Komuna = cattle.Municipality.Emri;



            var bloodPressure = cattle.CattleBloodPressure.ToList();

            foreach (var item in bloodPressure)
            {
                model.CattleBloodPressure.Add(new ViewModels.CattleBloodPressure.CattleBloodPressureViewModel
                {
                    PressureFrom = item.SystolicValue,
                    PressureTo = item.DiastolicValue,
                    DateMeasured = item.DateMeasured.ToString("dd/MM/yyyy HH:mm")
                });
            }

            //po i kthejm temperaturat e cattles si liste te tipit List<CattleTemperature>
            var temperatures = cattle.CattleTemperature.ToList();

            //listen e nxjerrur me larte e kthejm ne tipin per view
            //dhe po e shtim ne model
            model.CattleTemp = (from t in temperatures
                               select new CattleTempViewModel { 
                                Temperature = t.Temperature,
                                DateMeasured = t.DateMeasured,
                               }).ToList();

            model.CattleHumidity = cattle.CattleHumidity.Select(q => new HumidityViewModel { 
                Humidity = q.Humidity,
                DateMeasured = q.DateMeasured
            }).ToList();

            model.CattlePosition = cattle.CattlePosition.Select(q => new PositionViewModel { 
                Id = q.Id,
                Lat = q.Lat,
                Long = q.Long
            }).ToList();

            model.CattleMilk = cattle.CattleMilk.Select(q => new ViewModels.Milk.MilkViewModel
            {
                Identifier = q.Identifier.ToString(),
                DateCollected = q.Created.ToLongDateString(),
                LitersCollected = q.LitersCollected,
                Price = q.Price,
                TotalProfit = q.LitersCollected * q.Price,

            }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string ide)
        {
            if (ide == null)
            {
                return BadRequest();
            }

            int id = AesCrypto.Decrypt<int>(ide);
            var cat = _db.Cattle.Find(id);

            // Gender ViewBag
            var genderList = new List<SelectListItem>
    {
        new SelectListItem { Value = "Mashkull", Text = "Mashkull" },
        new SelectListItem { Value = "Femer", Text = "Femer" }
    };
            ViewBag.Gender = genderList;

            // Breed ViewBag
            var breeds = _db.Breed.ToList();
            var breedList = new SelectList(breeds, "Id", "Name", cat.BreedId);
            ViewBag.Breed = breedList;

            // Farm ViewBag
            var farms = _db.Farm.ToList();
            var farmList = new SelectList(farms, "Id", "Name", cat.FarmId);
            ViewBag.Farms = farmList;

            if (cat == null)
            {
                return NotFound();
            }

            CattleEditViewModel editViewModel = new CattleEditViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                Weight = cat.Weight,
                BreedId = cat.BreedId,
                BirthDate = cat.BirthDate.ToShortDateString(),
                Gender = cat.Gender,
                FarmId = cat.FarmId
            };

            return PartialView(editViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CattleEditViewModel viewModel)
        {
            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Lageshtia u ndryshua me sukses", Title = "Sukses" };
            if (!ModelState.IsValid)
            {
                var cat = _db.Cattle.Find(viewModel.Id);
                if (cat == null)
                {
                    return NotFound();
                }

                cat.Name = viewModel.Name;
                cat.Weight = viewModel.Weight;
                cat.BreedId = viewModel.BreedId;
                cat.BirthDate = DateTime.Parse(viewModel.BirthDate);
                cat.Gender = viewModel.Gender;
                cat.FarmId = viewModel.FarmId;
                _db.Update(cat);
                _db.SaveChanges();
                return Json(error);
            }

            var genderList = new List<SelectListItem>
    {
        new SelectListItem { Value = "Mashkull", Text = "Mashkull" },
        new SelectListItem { Value = "Femer", Text = "Femer" }
    };
            ViewBag.Gender = genderList;

            var breeds = _db.Breed.ToList();
            var breedList = new SelectList(breeds, "Id", "Name", viewModel.BreedId);
            ViewBag.Breed = breedList;

            var farms = _db.Farm.ToList();
            var farmList = new SelectList(farms, "Id", "Name", viewModel.FarmId);
            ViewBag.Farms = farmList;

            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {

            if (Id == null)
            {
                return BadRequest();
            }


            var cattle = _db.Cattle.Find(Id);
            if (cattle != null)
            {
                var result = _db.Cattle.Remove(cattle);

                await _db.SaveChangesAsync();

            }

            return Json("success");

        }
    }
}