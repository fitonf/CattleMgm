using CattleMgm.ViewModels.Menu;
using Microsoft.Build.Framework;

namespace CattleMgm.ViewModels.Municipality
{
    public class MunicipalityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? Zip { get; set; }
    }

    public class MunicipalityCreateViewModel
    {
        [Required]

        public string Name { get; set; }


        public int? Zip { get; set; }
    }
    public class MunicipalityEditViewModel : MunicipalityCreateViewModel
    {
        public int Id { get; set; }
    }

}
