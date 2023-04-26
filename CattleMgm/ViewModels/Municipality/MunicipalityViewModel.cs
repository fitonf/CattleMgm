using Microsoft.Build.Framework;

namespace CattleMgm.ViewModels.Municipality
{
    public class MunicipalityViewModel
    {
        public string Name { get; set; }

        public int? Zip { get; set; }
    }

    public class MunicipalityCreateViewModel
    {
        [Required]
        public string Name { get; set; }


        public int? Zip { get; set; }
    }

    
}
