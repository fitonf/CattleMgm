using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.Media
{
    public class MediaViewModel
    {
        
        public IFormFile document { get; set; }

    }
    public class MediaListViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Path { get; set; }
        public int Type { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

    }

}
