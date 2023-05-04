namespace CattleMgm.Models
{
    public class CattleReportModel
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string BreedId { get; set; }
        public DateTime BrithDate { get; set; }
        public string Gender { get; set; }
        public string FarmId { get; set;}
        public string? MunicipalityId { get; set; }

    }
}
