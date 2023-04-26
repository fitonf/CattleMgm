using Microsoft.Build.Framework;

namespace CattleMgm.ViewModels.Breed
{
    public class BreedViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Type { get; set; }
    }
    public class BreedCreateViewModel
    {

        [Required]
        public string Name { get; set; }

        public int? Type { get; set; }
    }
    public class BreedEditViewModel :BreedCreateViewModel
    {
        public int Id { get; set; }
    }

}
