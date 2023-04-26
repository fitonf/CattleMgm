using CattleMgm.Data.Entities;
using CattleMgm.Data;
using CattleMgm.Models;
using CattleMgm.Repository.Media;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CattleMgm.ViewModels.Milk;
using CattleMgm.Repository.Cattles;
using Microsoft.AspNetCore.Mvc.Rendering;
using CattleMgm.Helpers.Security;
using CattleMgm.ViewModels.Menu;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Submenu;
using CattleMgm.Repository.Milk;

namespace CattleMgm.Controllers
{
    public class MilkController : BaseController
    {
        public IMilkRepository _MilkRepository;
        public MilkController(ApplicationDbContext context
            , praktikadbContext db
            , UserManager<ApplicationUser> userManager
            , IMilkRepository milkRepository) : base(context, db, userManager)
        {
            _MilkRepository = milkRepository;
        }
        public async Task<IActionResult> Index()
        {
            var milk = new List<CattleMilk>();
            milk = await _MilkRepository.GetAllMilk();

            List<MilkViewModel> model = new List<MilkViewModel>();

            foreach (var item in milk)
            {
                model.Add(new MilkViewModel
                {
                    Id = item.Id,
                    CattleName = item.Cattle.Name,
                    Identifier = item.Cattle.UniqueIdentifier.ToString(),
                    DateCollected = item.Created.ToString("dd/MM/yyyy HH:mm"),
                    LitersCollected = item.LitersCollected,
                    Price = item.Price
                });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cattles = new List<Cattle>();
            var farmerId = await _db.Farmer.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            //if (!User.IsInRole("Administrator"))
            //{
            //    cattles = _cattleRepository.GetCattlesByFarmerId(farmerId.Id);
            //}
            //else
            //{
            //    cattles = _cattleRepository.GetCattles();
            //}
            ViewBag.Cattles = new SelectList(cattles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MilkCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ka ndodhur nje gabim. Plotesoni te dhenat obligative");
                return View(model);
            }

            CattleMilk cattleMilk = new CattleMilk();
            cattleMilk.Identifier = Guid.NewGuid();
            cattleMilk.LitersCollected = model.LitersCollected;
            cattleMilk.Price = model.Price;
            cattleMilk.Created = DateTime.Now;
            cattleMilk.CreatedBy = user.Id;
            cattleMilk.CattleId = model.CattleId;

            await _db.CattleMilk.AddAsync(cattleMilk);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult _Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //int did = AesCrypto.Decrypt<int>(ide);

            var milk = _db.CattleMilk.Find(id);


            if (milk == null)
            {
                return NotFound();
            }

            MilkEditViewModel editViewModel = new MilkEditViewModel
            {
                Id = milk.Id,
                CattleId = milk.CattleId,
                Price = milk.Price,
                LitersCollected = milk.LitersCollected

            };

            return PartialView(editViewModel);
        }

        [HttpPost]
        public IActionResult _Edit(MilkEditViewModel model)
        {
            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Milk forma eshte modifikuar me sukses", Title = "Sukses" };

            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
                return Json(error);
            }
            var milk = _db.CattleMilk.Find(model.Id);
            if (milk == null)
            {
                return NotFound();

            }
            milk.Id = model.Id;
            milk.Price = model.Price;
            milk.LitersCollected = model.LitersCollected;

            _db.Update(milk);

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


            var milk = _db.CattleMilk.Find(Id);
            if (milk != null)
            {
                var result = _db.CattleMilk.Remove(milk);

                await _db.SaveChangesAsync();

            }

            return Json("success");

        }
    }
}