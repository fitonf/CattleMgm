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
                                                           ,Id=x.Id
                                                           ,CattleId=x.CattleId
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
        [HttpGet]
        public IActionResult _Edit(int CattleId, int ide)
        {
            if (ide == null)
            {
                return BadRequest();
            }
            //int did = AesCrypto.Decrypt<int>(ide);

            var milk = _db.CattleMilk.Find(ide);


            if (milk == null)
            {
                return NotFound();
            }

            MilkEditViewModel editViewModel = new MilkEditViewModel
            {
                CattleId = milk.CattleId,
               Price=milk.Price,
               LitersCollected=milk.LitersCollected
              
            };
            return View(editViewModel);
        }

        [HttpPost]
        public IActionResult _Edit(SubmenuEditViewModel model)
        {
            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Submenu eshte modifikuar me sukses", Title = "Sukses" };

            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
                return Json(error);
            }
            var submenu = _db.SubMenu.Find(model.Id);
            if (submenu == null)
            {
                return NotFound();

            }
            submenu.Action = model.Action;
            submenu.Controller = model.Controller;
            submenu.Area = model.Area;
            submenu.Claim = model.Policy;
            submenu.Icon = model.Icon;
            submenu.NameSq = model.NameSq;
            submenu.NameEn = model.NameEn;
            submenu.NameSr = model.NameSr;
            submenu.OrdinalNumber = model.OrdinalNumber;
            submenu.StaysOpenFor = model.StaysOpenFor;
            submenu.ParentSubId = model.ParentID;

            _db.Update(submenu);

            _db.SaveChanges();

            return Json(error);

        }
    }
}