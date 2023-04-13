using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Menu;
using Microsoft.AspNetCore.Mvc;

namespace CattleMgm.Controllers
{
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
        public IActionResult _Create(MenuCreateViewModel model)
        {
            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Menu eshte regjistruar me sukses" }; 
            
            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative" };
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
    }
}
