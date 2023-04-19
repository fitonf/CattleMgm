using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.CattleBloodPressure
{
    public class CattleBloodPressureViewModel
    {
        public int Id { get; set; }
        public string CattleName { get; set; }
        public int PresureFrom { get; set; }
        public int PresureTo { get; set; }
        public  string DateMeasured { get; set; }
    }
    
 

   public class CattleBloodPressureCreateViewModel
    {
        [Required]
        [Display(Name = "Cattle name")]
        public int CattleId { get; set; }
        [Required]
        [Display(Name = "Presure From")]
        public int PresureFrom { get; set; }
        [Required]
        [Display(Name = "Presure To")]
        public int PresureTo { get; set; }
        //[Required]
        //[Display(Name = "Data Measured")]
        //public string DateMeasured { get; set; }
    }
    
}
