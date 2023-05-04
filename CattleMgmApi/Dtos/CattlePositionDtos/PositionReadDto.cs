namespace CattleMgmApi.Dtos.CattlePositionDtos
{
    public class PositionReadDto
    {
        public int Id { get; set; }

        public string CattleId { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public DateTime DateMeasured { get; set; }
    }
}
