using CattleMgmApi.Repository;
using CattleMgmApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgmApi.Repository.Media
{

    public class MediaRepository : IMediaRepository
    {

        public PraktikadbContext _db;
        public IWebHostEnvironment _env;
        public MediaRepository(PraktikadbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task CreateMedia(Data.Entities.Media media)
        {
            if (media == null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            await _db.Media.AddAsync(media);
        }

        public void DeleteMedia(Data.Entities.Media media)
        {
            if (media == null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            _db.Media.Remove(media);
        }

        public async Task<IEnumerable<Data.Entities.Media>> GetAllMedia()
        {
            var media = await _db.Media.ToListAsync();

            return media;
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}