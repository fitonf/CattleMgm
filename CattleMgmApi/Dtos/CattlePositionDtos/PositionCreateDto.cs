namespace CattleMgmApi.Dtos.CattlePositionDtos
{
    public class PositionCreateDto
    {

        public int CattleId { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public DateTime DateMeasured { get; set; }

        public string CreatedBy { get; set; }
    }
}
