using CattleMgm.Data.Entities;
using CattleMgm.ViewModels.Media;

namespace CattleMgm.Repository.Media
{
    public interface IMediaRepository
    {
        Task<bool> UploadFile(IFormFile file, string path);

        Task<CattleMgm.Data.Entities.Media> AddFiletoDb(IFormFile file, string shtegu,string id);
        Task <bool> DeleteFile(int id);
        Task<List<MediaListViewModel>> GetAllFiles();
        Task<MediaListViewModel> GetMedia(int id);
    }
}
