using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers.Security;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CattleMgm.Controllers
{
    [Authorize(Policy = "ro:1")]
    public class MenuController : BaseController
    {
        public MenuController(ApplicationDbContext context, praktikadbContext db) : base(context, db)
        {

        }

        public IActionResult Index()
        {
            var menus = _db.Menu.Select(q => new MenuViewModel { 
                Id = q.MenuId,
                Name = q.NameSq,
                Area = q.Area,
                Action = q.Action,
                Controller = q.Controller,
                Icon = q.Icon,
                HasSubMenu = q.HasSubMenu
            }).ToList();

            return View(menus);
        }

        [HttpGet]
        public IActionResult _Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult _Create(MenuCreateViewModel model)
        {
            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Menu eshte regjistruar me sukses", Title = "Sukses" }; 
            
            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
                return Json(error);
            }

            Menu add = new Menu() { 
                NameSq = model.Name_SQ,
                NameEn = model.Name_EN,
                NameSr = model.Name_SR,
                Area = model.Area,
                Action = model.Action,
                Controller = model.Controller,
                HasSubMenu = model.HasSubMenu,
                IsActive = true,
                InsertedFrom = "a9b31e68-99ab-4b6d-96ea-8bc396d6de21",
                InsertedDate = DateTime.Now,
                OrdinalNumber = model.OrdinalNumber,
                StaysOpenFor = model.StaysOpenFor,
                Icon = model.Icon,
                Claim = model.Policy,
                ClaimType = model.Policy != null ? model.Policy.Split(':')[0] : null,
            };


            _db.Menu.Add(add);
            _db.SaveChanges();

            return Json(error);
        }

        [HttpGet]
        public IActionResult _Edit(string? id)
        {
            
            if(id == null)
            {
                return BadRequest();
            }

            int did = AesCrypto.Decrypt<int>(id);

            var menu = _db.Menu.Find(did);

            if(menu == null)
            {
                return NotFound();
            }

            MenuEditViewModel editViewModel = new MenuEditViewModel {
                Id = menu.MenuId,
                Action = menu.Action,
                Controller = menu.Controller,
                HasSubMenu = menu.HasSubMenu,
                Area = menu.Area,
                Icon = menu.Icon,
                Name_EN = menu.NameEn,
                Name_SQ = menu.NameSq,
                Name_SR = menu.NameSr,
                OrdinalNumber = menu.OrdinalNumber,
                Policy = menu.Claim,
                StaysOpenFor = menu.StaysOpenFor
            };

            return PartialView(editViewModel);
        }

        [HttpPost]
        public IActionResult _Edit(MenuEditViewModel model)
        {

            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Menu eshte modifikuar me sukses", Title = "Sukses" };

            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
                return Json(error);
            }

            var menu = _db.Menu.Find(model.Id);

            if(menu == null)
            {
                return NotFound();
            }

            menu.NameSq = model.Name_SQ;
            menu.NameSr = model.Name_SR;
            menu.NameEn = model.Name_EN;
            menu.Action = model.Action;
            menu.Controller = model.Controller;
            menu.Icon = model.Icon;
            menu.Area = model.Area;
            menu.Claim = model.Policy;
            menu.ClaimType = model.Policy != null ? model.Policy.Split(':')[0] : null;
            menu.UpdatedDate = DateTime.Now;
            menu.UpdatedFrom = "a9b31e68-99ab-4b6d-96ea-8bc396d6de21";
            menu.HasSubMenu = model.HasSubMenu;
            menu.OrdinalNumber = model.OrdinalNumber;
            menu.StaysOpenFor = model.StaysOpenFor;

            _db.Update(menu);

            _db.SaveChanges();

            return Json(error);
        }
    }
}
