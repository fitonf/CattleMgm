using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Submenu;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CattleMgm.Controllers
{
    public class SubmenuController : BaseController
    {
        public SubmenuController(ApplicationDbContext context,
            praktikadbContext db,
            UserManager<ApplicationUser> userManager) : base(context, db, userManager)
        {

        }

        public IActionResult _Index(int id)
        {
            var submenus = _db.SubMenu.Where(q => q.MenuId == id).Select(q => new SubmenuViewModel {
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

            var submenus = _db.SubMenu.Where(q => q.MenuId == menu.MenuId).Select(q => new { q.SubmenuId, q.NameSq }).ToList();

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
    
    [HttpGet]
    public IActionResult _Edit(int mid, int ide)
    {
        if (ide == null)
        {
            return BadRequest();
        }
        //int did = AesCrypto.Decrypt<int>(ide);

        var submenu = _db.SubMenu.Find(ide);


        if (submenu == null)
        {
            return NotFound();
        }

        SubmenuEditViewModel editViewModel = new SubmenuEditViewModel
        {
            Id = submenu.SubmenuId,
            MId = submenu.MenuId,
            Menu = AesCrypto.Enkrypt(submenu.MenuId),
            Action = submenu.Action,
            Controller = submenu.Controller,
            Area = submenu.Area,
            Policy = submenu.Claim,
            Icon = submenu.Icon,
            IsActive = true,
            NameSq = submenu.NameSq,
            NameEn = submenu.NameEn,
            NameSr = submenu.NameSr,
            OrdinalNumber = submenu.OrdinalNumber,
            StaysOpenFor = submenu.StaysOpenFor,
            ParentID = submenu.ParentSubId
        };
        return PartialView(editViewModel);
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
