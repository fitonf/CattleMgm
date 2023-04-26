using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Data.Migrations;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Municipality;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CattleMgm.Controllers
{
    [AllowAnonymous]
    public class MunicipalityController : BaseController
    {
        public MunicipalityController(ApplicationDbContext context, praktikadbContext db, UserManager<ApplicationUser> userManager) : base(context, db, userManager)
        {

        }
        public IActionResult Index()
        {
            var municipalities = _db.Municipality.Select(x => new MunicipalityViewModel { Id = x.Id, Name = x.Emri, Zip = x.Zip }).OrderBy(x => x.Zip).ToList();

            List<MunicipalityViewModel> model = new List<MunicipalityViewModel>();

            //ekuivalentja me LINQ nga rreshti 21
            //foreach (var item in municipalities)
            //{
            //	model.Add(new MunicipalityViewModel
            //	{
            //		Name = item.Name,
            //		Zip = item.Zip,
            //	});
            //}
            return View(municipalities);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MunicipalityCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ka ndodhur nje gabim. Plotesoni te dhenat obligative");
                return View(model);
            }

            Municipality municipality = new Municipality();

            municipality.Emri = model.Name;
            municipality.Zip = model.Zip;

            await _db.Municipality.AddAsync(municipality);
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

            var municipality = _db.Municipality.Find(id);


            if (municipality == null)
            {
                return NotFound();
            }

            MunicipalityEditViewModel editViewModel = new MunicipalityEditViewModel
            {
                Id = municipality.Id,
                Name = municipality.Emri,
                Zip = municipality.Zip

            };
            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(MunicipalityEditViewModel model)
        {


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Plotesoni fushat obligative");
                return View(model);
            }
            var municipality = _db.Municipality.Find(model.Id);
            if (municipality == null)
            {
                ModelState.AddModelError("", "Komuna nuk ekziston");
                return View(model);

            }
            municipality.Emri = model.Name;
            municipality.Zip = model.Zip;

            //var result= await

            _db.Update(municipality);

            _db.SaveChanges();

            return RedirectToAction("Index", "Municipality");

        }

       // [HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var municipality = _db.Municipality.Find(id);

        //    if (municipality == null)
        //    {
        //        ViewBag.ErrorMessage = $"Municipality with Id = {id} cannot be found";
        //        return View("Error");
        //    }
        //    else
        //    {
        //        var result = await Municipality.Remove(municipality);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index");
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }

        //        return View("Index");
        //    }

        //}
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {

            if (Id == null)
            {
                return BadRequest();
            }


            var municipality = _db.Municipality.Find(Id);
            if (municipality != null)
            {
                var result = _db.Municipality.Remove(municipality);

                await _db.SaveChangesAsync();

            }

            return Json("success");

        }


        public IActionResult _Edit(MunicipalityEditViewModel model)
        {
            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Komuna eshte modifikuar me sukses", Title = "Sukses" };

            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
                return Json(error);
            }
            var municipality = _db.Municipality.Find(model.Id);
            if (municipality == null)
            {
                return NotFound();

            }
            municipality.Emri = model.Name;
            municipality.Zip = model.Zip;


            _db.Update(municipality);

            _db.SaveChanges();

            return Json(error);

        }
    }
}
