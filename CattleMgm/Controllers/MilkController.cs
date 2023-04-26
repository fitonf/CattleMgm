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

namespace CattleMgm.Controllers
{
    public class MilkController : BaseController
    {
        public ICattleRepository _cattleRepository;
        public MilkController(ApplicationDbContext context
            , praktikadbContext db
            , UserManager<ApplicationUser> userManager
            , ICattleRepository cattleRepository) : base(context, db, userManager)
        {
            _cattleRepository = cattleRepository;
        }
        public IActionResult Index()
        {

            var model = _db.CattleMilk.Include(x=>x.Cattle).Select(x=>new MilkViewModel { Identifier = x.Identifier.ToString()
                                                           ,CattleName = x.Cattle.Name
                                                           ,LitersCollected= x.LitersCollected
                                                           ,Price = x.Price
                                                           ,DateCollected = x.Created.ToShortDateString()
                                                           ,TotalProfit = x.Price*x.LitersCollected}).ToList();


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cattles = new List<Cattle>();
            var farmerId = await _db.Farmer.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (!User.IsInRole("Administrator"))
            {
                cattles = _cattleRepository.GetCattlesByFarmerId(farmerId.Id);
            }
            else
            {
                cattles = _cattleRepository.GetCattles();
            }
            ViewBag.Cattles = new SelectList(cattles,"Id","Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MilkCreateViewModel model)
        {
            if(!ModelState.IsValid)
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
    }
}
