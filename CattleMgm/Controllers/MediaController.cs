using CattleMgm.Data.Entities;
using CattleMgm.Data;
using CattleMgm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CattleMgm.ViewModels.Media;
using CattleMgm.Repository.Media;

namespace CattleMgm.Controllers
{
    [AllowAnonymous]
    public class MediaController : BaseController
    {
        public IMediaRepository _mediaRepository { get; set; }
        public MediaController(ApplicationDbContext context
            , praktikadbContext db
            , UserManager<ApplicationUser> userManager
            , IMediaRepository mediaRepository) : base(context, db, userManager)
        {
            _mediaRepository = mediaRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(MediaViewModel media)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ka ndodhur nje gabim. Plotesoni te dhenat obligative");
                return View(media);
            }

           var success = await _mediaRepository.UploadFile(media.document, Directory.GetCurrentDirectory() + $"\\Dokumentet\\");

            if (!success)
            {
                ModelState.AddModelError("", "Dokumenti nuk eshte ngarkuar.Ri-ngarkoni dokumentin.");
                return View(media);
            }

            await _mediaRepository.AddFiletoDb(media.document, Directory.GetCurrentDirectory() + $"\\Dokumentet\\", user.Id);

            return RedirectToAction("Index");
        }
    }
}
