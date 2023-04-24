using CattleMgm.Data.Entities;


namespace CattleMgm.Repository.Media
{
    public interface IMediaRepository
    {
        Task<bool> UploadFile(IFormFile file, string path);

        Task<CattleMgm.Data.Entities.Media> AddFiletoDb(IFormFile file, string shtegu,string id);
    }
}
