using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.Position
{
    public class PositionViewModel
    {
        public int Id { get; set; }

        public string CattleName { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }
        public string DateMeasured { get; set; }
        public DateTime Date { get; set; }
    }

    public class PositionCreateViewModel
    {
        [Required(ErrorMessage = "Ju lutem zgjedhni gjedhen")]
        [Display(Name = "Emri i Gjedhes")]
        public int CattleId { get; set; }

        [Required(ErrorMessage = "Kjo fushe eshte obligative")]
        [Display(Name = "Latitude")]
        public double Lat { get; set; }

        [Required(ErrorMessage = "Kjo fushe eshte obligative")]
        [Display(Name = "Longitude")]
        public double Long { get; set; }

    }

    public class PositionEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kjo fushë eshte obligative")]
        [Display(Name = "Id")]
        public string CattleId { get; set; }

        [Required(ErrorMessage = "Kjo fushë eshte obligative")]
        [Display(Name = "Latitude")]
        public double Lat { get; set; }

        [Required(ErrorMessage = "Kjo fushë eshte obligative")]
        [Display(Name = "Longitude")]
        public double Long { get; set; }
    }

    public class PositionReportModel
    {
        public string Id { get; set; }

        public string CattleName { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

    }
    public class SearchPosition
    {
        public int? CattleId { get; set; }
        public double? Lat { get; set; }
        public double? Long { get; set; }
        public string? DateMeasured { get; set; }
    }
}
