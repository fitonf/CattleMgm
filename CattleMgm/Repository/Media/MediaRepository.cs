using CattleMgm.Data.Entities;
using CattleMgm.Helpers;
using CattleMgm.Models;
using CattleMgm.ViewModels.Cattle;
using CattleMgm.ViewModels.Media;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using Microsoft.AspNetCore.Hosting;

namespace CattleMgm.Repository.Media
{
    public class MediaRepository : IMediaRepository
    {
        
        public praktikadbContext _db;
        public IWebHostEnvironment _env;
        public MediaRepository(praktikadbContext db, IWebHostEnvironment env)
        {
            _db= db;
            _env = env;
        }

        public async Task<Data.Entities.Media> AddFiletoDb(IFormFile dokumenti,string shtegu, string id)
        {
            if(dokumenti == null)
            {
                throw new ArgumentNullException(nameof(dokumenti));
            }

            Data.Entities.Media media = new Data.Entities.Media();
            media.Name = dokumenti.FileName;
            media.Identifier = Guid.NewGuid();
            media.Path = shtegu + dokumenti.FileName;
            media.Type = 1;
            media.Created = DateTime.Now;
            media.CreatedBy = id;

            await _db.Media.AddAsync(media);

            await _db.SaveChangesAsync();

            return media;

        }
        public async Task<List<MediaListViewModel>> GetAllFiles()
        {
            var media = await _db.Media.ToListAsync();
            var mediaViewModels = new List<MediaListViewModel>();

            foreach (var m in media)
            {
                var mediaViewModel = new MediaListViewModel()
                {
                    id = m.Id,
                    Identifier = m.Identifier.ToString(),
                    Name = m.Name,
                    Path = m.Path,
                    Type = m.Type,
                    Created = m.Created,
                    CreatedBy = m.CreatedBy,
                };

                mediaViewModels.Add(mediaViewModel);
            }

            return mediaViewModels;
        }

        public async Task<MediaListViewModel> GetMedia(int id)
        {
            var media = await _db.Media.FindAsync(id);

            if (media == null)
            {
                return null;
            }

            var mediaViewModel = new MediaListViewModel()
            {
                Identifier = media.Identifier.ToString(),
                Name = media.Name,
                Path = media.Path,
                Created = media.Created
            };

            return mediaViewModel;
        }

        public async Task<bool> UploadFile(IFormFile file, string path)
        {
            try
            {
                if(file.Length > 0)
                {
                    //Nqs dojme ta kufizojme madhesine e dokumentit
                    //if(file.Length > 25000)
                    //{
                    //    Console.WriteLine("Nuk lejohet me shume se 25 mb!");
                    //    return false;
                    //}
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var filePath = Path.Combine(path,
                       file.FileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                return false;
            }

            return false;
        }

        public async Task<bool> DeleteFile(int id)
        {
            try
            {
                var media = await _db.Media.FindAsync(id);

                if (media != null)
                {
                    var uploadsPath = Path.Combine(_env.ContentRootPath, "Dokumentet");
                    var filePath = Path.Combine(uploadsPath, media.Name);
                    if (File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);

                    }
                    _db.Media.Remove(media);
                    await _db.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}
