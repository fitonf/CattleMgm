using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Data.Migrations;
using CattleMgm.Models;
using CattleMgm.ViewModels.Municipality;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CattleMgm.Controllers
{
	[AllowAnonymous]
	public class MunicipalityController : BaseController
	{
			public MunicipalityController(ApplicationDbContext context, praktikadbContext db, UserManager<ApplicationUser> userManager) :base(context,db,userManager)
			{

			}
		public IActionResult Index()
		{
			var municipalities = _db.Municipality.Select(x => new MunicipalityViewModel { Name = x.Emri, Zip = x.Zip }).OrderBy(x=>x.Zip).ToList();

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
			if(!ModelState.IsValid)
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

	}
}
