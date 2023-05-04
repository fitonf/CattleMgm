namespace CattleMgm.Models
{
    public class BreedReportModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Type { get; set; }
    }

    public class SearchBreed
    {
       
        public string? Name { get; set; }
        public int? Type { get; set; }
    }
}
