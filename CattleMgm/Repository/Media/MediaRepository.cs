using CattleMgm.Data.Entities;
using CattleMgm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CattleMgm.Repository.Media
{
    public class MediaRepository : IMediaRepository
    {
        public praktikadbContext _db;
        public MediaRepository(praktikadbContext db)
        {
            _db= db;
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
    }
}
