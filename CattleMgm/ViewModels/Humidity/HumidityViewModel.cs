using CattleMgm.Controllers;
using CattleMgm;
using System.ComponentModel.DataAnnotations;
using CattleMgm.Data;

namespace CattleMgm.ViewModels.Humidity
{
    public class HumidityViewModel
    {
        public int Id { get; set; }

        public int CattleId { get; set; }

        public string CattleName { get; set; }

        public double Humidity { get; set; }

        public DateTime DateMeasured { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
    }

    public class HumidityCreateViewModel
    {


        [Display(Name = "Gjedhi")]
        [Required(ErrorMessage = "Zgjidhni gjedhen.")]
        public int CattleId { get; set; }

        [Display(Name = "Lageshtia")]
        [Required(ErrorMessage = "Caktoni lageshtinë.")]
        public double Humidity { get; set; }

        [Display(Name = "Data")]
        public int DateMeasured { get; set; }


        [Display(Name = "Krijuar nga")]
        public int CreatedBy { get; set; }



    }
    public class HumidityEditViewModel : HumidityCreateViewModel
    {
        public int Id { get; set; }

    }
}