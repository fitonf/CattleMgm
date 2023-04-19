using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.Farm
{
    public class FarmViewModel
    {
        public string FarmerName { get; set; }

        public string FarmName { get; set; }

        public string Place { get; set; }

        public string Address { get; set; }

        public bool Active { get; set; }
    }

    public class FarmCreateViewModel
    {
        [Required(ErrorMessage = "Ju lutem zgjedhni fermerin")]
        [Display(Name = "Fermeri")]
        public int FarmerId { get; set; }

        [Display(Name = "Emri i fermes")]
        public string FarmName { get; set; }

        [Display(Name = "Vendi")]
        public string Place { get; set; }

        [Display(Name = "Adresa")]
        public string Address { get; set; }

    }
}
