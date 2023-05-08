namespace CattleMgmApi.Dtos.TempDtos
{
    public class CattleTempCreateDto
    {
        public int CattleId { get; set; }

        public double Temperature { get; set; }

        public DateTime DateMeasured {get; set;}

        public string CreatedBy { get; set;}
    
    }
}
