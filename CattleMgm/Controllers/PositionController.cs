using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.Repository.Farm;
using CattleMgm.Repository.Position;
using CattleMgm.ViewModels;
using CattleMgm.ViewModels.Farm;
using CattleMgm.ViewModels.Menu;
using CattleMgm.ViewModels.Position;
using CattleMgm.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Controllers
{
    public class PositionController : BaseController
    {
        public IPositionRepository _posRepository;
        public PositionController(ApplicationDbContext context,
            praktikadbContext db,
            UserManager<ApplicationUser> userManager,
            IPositionRepository posRepository) : base(context, db, userManager)
        {
            _posRepository = posRepository;
        }

        public async Task<IActionResult> Index()
        {
            var positions = new List<CattlePosition>();
            positions = await _posRepository.GetAllPositions();
            

            List<PositionViewModel> model = new List<PositionViewModel>();

            foreach (var item in positions)
            {
                model.Add(new PositionViewModel
                {
                    Id=item.Id,
                    CattleName = $"{item.Cattle.Name}",
                    Lat = item.Lat,
                    Long = item.Long

                });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var positions = await _db.Cattle.Select(q => new { q.Id, FullName = q.Name })
                                .ToListAsync();
            ViewBag.Cattles = new SelectList(positions, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ka ndodhur nje gabim. Plotesoni te dhenat obligative");
                return View(model);
            }
            
            CattlePosition pos = new CattlePosition();
            pos.CattleId = model.CattleId;
            pos.Lat = model.Lat;
            pos.Long = model.Long;
            pos.DateMeasured = DateTime.Now;
            pos.CreatedBy = user.Id;

            await _db.CattlePosition.AddAsync(pos);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        //estrdyuiojhgfgghvjbj

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }

            //int did = AesCrypto.Decrypt<int>(ide);

            var pos = _db.CattlePosition.Find(id);

            if (pos == null)
            {
                return NotFound();
            }

            PositionEditViewModel editViewModel = new PositionEditViewModel
            {
                CattleId = pos.CattleId.ToString(),
                Lat = pos.Lat,
                Long = pos.Long
            };

            return PartialView(editViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PositionEditViewModel model)
        {

            ErrorViewModel error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Success, ErrorDescription = "Menu eshte modifikuar me sukses", Title = "Sukses" };

            if (!ModelState.IsValid)
            {
                error = new ErrorViewModel { ErrorNumber = Helpers.ErrorStatus.Warning, ErrorDescription = "Plotesoni te dhenat obligative", Title = "Lajmerim" };
                return Json(error);
            }

            var pos = _db.CattlePosition.Find(model.Id);

            if (pos == null)
            {
                return NotFound();
            }

            pos.Id = model.Id;
            pos.Lat = model.Lat;
            pos.Long = model.Long;

            _db.Update(pos);

            _db.SaveChanges();

            return Json(error);
        }
    }
}
