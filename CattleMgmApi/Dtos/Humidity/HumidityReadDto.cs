namespace CattleMgmApi.Dtos.Humidity
{
    public class HumidityReadDto
    {
        public int Id { get; set; }

        public int CattleId { get; set; }
        public double Humidity { get; set; }

    }
}
