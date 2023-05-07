using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.CattleBloodPressure
{
    public class CattleBloodPressureViewModel
    {
        public int Id { get; set; }
        public string CattleName { get; set; }
        public int PressureFrom { get; set; }
        public int PressureTo { get; set; }
        public string DateMeasured { get; set; }
        
    }
    public class CattleBloodPressureCreateViewModel
    {
        [Required]
        public int CattleId { get; set; }
        [Required]
        [Remote(action: "IsBloodPressureAvailable", controller: "CattleBloodPressure", AdditionalFields = nameof(CattleId) + "," + nameof(PressureFrom) + "," + nameof(PressureTo))]
        public int PressureFrom { get; set; }
        [Required]
        [Remote(action: "IsBloodPressureAvailable", controller: "CattleBloodPressure", AdditionalFields = nameof(CattleId) + "," + nameof(PressureFrom) + "," + nameof(PressureTo))]
        public int PressureTo { get; set; }
        
        //public string DateMeasured { get; set; }

    }

    public class CattleBloodPressureEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public int CattleId { get; set; }
        [Required]
        public int PressureFrom { get; set; }
        [Required]
        public int PressureTo { get; set; }
        [Required]
        public string DateMeasured { get; set; }

    }
}
