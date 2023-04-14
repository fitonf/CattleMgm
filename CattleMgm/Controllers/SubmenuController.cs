using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Submenu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CattleMgm.Controllers
{
    public class SubmenuController : BaseController
    {
        public SubmenuController(ApplicationDbContext context, praktikadbContext db) : base(context, db)
        {

        }

        public IActionResult _Index(int id)
        {
            var submenus = _db.SubMenu.Where(q => q.MenuId == id).Select(q => new SubmenuViewModel{ 
                 Id = q.SubmenuId,
                 MId = q.MenuId,
                 Action = q.Action,
                 Controller = q.Controller,
                 Area = q.Area,
                 Name = q.NameSq
            }).ToList();

            return PartialView(submenus);
        }

        [HttpGet]
        public IActionResult _Create(int id)
        {
            var menu = _db.Menu.Find(id);

            SubmenuCreateViewModel model = new SubmenuCreateViewModel()
            {
                MId = menu.MenuId,
                Menu = menu.NameSq,
                IsActive = true
            };

            var submenus = _db.SubMenu.Where(q => q.MenuId == menu.MenuId).Select(q => new { q.SubmenuId, q.NameSq}).ToList();

            ViewBag.ParentId = new SelectList(submenus, "SubmenuId", "NameSq");

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult _Create(SubmenuCreateViewModel model)
        {
            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Menu eshte regjistruar me sukses", Title = "Sukses" };

            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
                return Json(error);
            }

            SubMenu subMenu = new SubMenu {
                MenuId = model.MId,
                Action = model.Action,
                Controller = model.Controller,
                Area = model.Area,
                Claim = model.Policy,
                ClaimType = model.Policy != null ? model.Policy.Split(':')[0] : null,
                Icon = model.Icon,
                IsActive = true,
                NameSq = model.NameSq,
                NameEn = model.NameEn,
                NameSr = model.NameSr,
                OrdinalNumber = model.OrdinalNumber,
                StaysOpenFor = model.StaysOpenFor,
                ParentSubId = model.ParentID,
                InsertedDate = DateTime.Now,
                InsertedFrom = "a9b31e68-99ab-4b6d-96ea-8bc396d6de21",
                
            };

            _db.SubMenu.Add(subMenu);

            _db.SaveChanges();

            return Json(error);
        }
    }
}
