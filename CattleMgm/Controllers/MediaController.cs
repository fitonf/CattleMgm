using CattleMgm.Data.Entities;
using CattleMgm.Data;
using CattleMgm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CattleMgm.ViewModels.Media;
using CattleMgm.Repository.Media;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediaRepository.DeleteFile(id);

            if (!success)
            {
                ModelState.AddModelError("", "Dokumenti nuk eshte fshire.");
                return View("Index");
            }

            return RedirectToAction("DocumentList");
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DocumentListAsync()
        {
            ViewData["Title"] = "Lista e dokumenteve";

            var lista = await _mediaRepository.GetAllFiles();

            List<MediaListViewModel> listaViewModel = new List<MediaListViewModel>();

            foreach (var media in lista)
            {
                listaViewModel.Add(new MediaListViewModel
                {
                    id = media.id,
                    Name = media.Name,
                    Identifier = media.Identifier,
                    Path = media.Path,
                    Type = media.Type,
                    Created = media.Created,
                    CreatedBy = media.CreatedBy,
                }); ;
            }

            listaViewModel = listaViewModel.OrderByDescending(q => q.Name).ToList();

            return View(listaViewModel);
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

            return RedirectToAction("DocumentList");
        }
    }
}
