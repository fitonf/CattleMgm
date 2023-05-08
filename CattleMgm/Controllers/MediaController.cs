using CattleMgm.Data.Entities;
using CattleMgm.Data;
using CattleMgm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CattleMgm.ViewModels.Media;
using CattleMgm.Repository.Media;
using Microsoft.AspNetCore.Mvc.Rendering;
using CattleMgm.Models.Session;

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

        public IActionResult DocumentList()
        {
            ViewData["Title"] = "Lista e dokumenteve";
            var roles = _db.AspNetRoles
                                .Select(m => new { m.Name })
                                .ToList();
            ViewBag.Names = new SelectList(roles, "Name");
            //var lista = await _mediaRepository.GetAllFiles();

            //List<MediaListViewModel> listaViewModel = new List<MediaListViewModel>();

            //foreach (var media in lista)
            //{
            //    listaViewModel.Add(new MediaListViewModel
            //    {
            //        id = media.id,
            //        Name = media.Name,
            //        Identifier = media.Identifier,
            //        Path = media.Path,
            //        Type = media.Type,
            //        Created = media.Created,
            //        CreatedBy = media.CreatedBy,
            //    }); ;
            //}

            //listaViewModel = listaViewModel.OrderByDescending(q => q.Name).ToList();

            return View(/*listaViewModel*/);
        }
        public async Task<IActionResult> Lista()
        {
            return View();
        }
        public async Task<IActionResult> _Lista(SearchMediaModel model)
        {
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

            return Json(listaViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Upload(MediaViewModel media)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ka ndodhur nje gabim. Plotesoni te dhenat obligative");
                return View(media);
            }

            // Check the file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".txt", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx" };
            var fileExtension = Path.GetExtension(media.document.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("", "Formati i dokumentit nuk lejohet. Lejohen vetem fotografi, tekst dhe dokumente te Microsoft Office.");
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

        #region Report

        [HttpPost]
        public async Task<JsonResult> OpenIndexReport()
        {
            var media = _db.Media.ToList();
            var query = media
               .Select(q => new DocumentListReportModel
               {
                   Name = q.Name,
                   Type = q.Type,
                   Created = q.Created,
                   CreatedBy = _db.AspNetUsers.Where(t => t.Id == q.CreatedBy).Count() > 0 ? _db.AspNetUsers.Where(t => t.Id == q.CreatedBy).FirstOrDefault().UserName : "",
               }).ToList();


            HttpContext.Session.SetString("Path", "Reports\\MediaReport.rdl");
            HttpContext.Session.Set("queryresult", query);


            return Json(true);
        }
        #endregion
    }
}