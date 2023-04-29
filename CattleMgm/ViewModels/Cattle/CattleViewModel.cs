
using CattleMgm.Data.Entities;
using CattleMgm.ViewModels.CattleBloodPressure;
using CattleMgm.ViewModels.CattleTemperature;
using CattleMgm.ViewModels.Humidity;
using CattleMgm.ViewModels.Milk;
using CattleMgm.ViewModels.Municipality;
using CattleMgm.ViewModels.Position;
using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.Cattle
{
    public class CattleViewModel
    {
        public int Id { get; set; }

        public string UniqueIdentifier { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public string FarmerName { get; set; }

        public string FarmName { get; set; }

        public string Breed { get; set; }

        public string BirthDate { get; set; }

        public string Gender { get; set; }

        public string CreatedBy { get; set; }
    }

    public class CattleCreateViewModel
    {
        [Required]
        [Display(Name = "Ferma")]
        public int FarmId { get; set; }
        [Required]
        [Display(Name = "Lloji i gjedhes")]

        public int BreedId { get; set; }
        [Required]
        [Display(Name = "Emri i gjedhes")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Pesha")]
        public double Weight { get; set; } = 0;
        [Required]
        [Display(Name = "Gjinia")]
        public int Gender { get; set; }
        [Required]
        [Display(Name = "Data lindjes")]
        public string BirthDate { get; set; }
    }

    public class CattleDetailsViewModel
    {
        public CattleDetailsViewModel()
        {
            CattleBloodPressure = new List<CattleBloodPressureViewModel>();
            CattleTemp = new List<CattleTempViewModel>();
            CattleHumidity = new List<HumidityViewModel>();
            CattlePosition = new List<PositionViewModel>();
            CattleMilk = new List<MilkViewModel>();
        }

        public int Id { get; set; }

        public string UniqueIdentifier { get; set; }

        public bool MilkCollectedToday { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public string FarmerName { get; set; }

        public string FarmName { get; set; }

        public string Breed { get; set; }

        public string BirthDate { get; set; }

        public List<CattleTempViewModel> CattleTemp { get; set; }
        public List<CattleBloodPressureViewModel> CattleBloodPressure { get; set; }
        public List<HumidityViewModel> CattleHumidity { get; set; }
        public List<PositionViewModel> CattlePosition { get; set; }
        public List<MilkViewModel> CattleMilk { get; set; }

    }
    public class CattleEditViewModel : CattleCreateViewModel
    {
        public int Id { get; set; }
    }


}
