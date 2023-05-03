namespace CattleMgmApi.Dtos.Humidity
{
    public class HumidityCreateDto
    {
        public int CattleId { get; set; }
        public double Humidity { get; set; }
        public DateTime DateMeasured { get; set; }
        public string CreatedBy { get; set; }
    }
}
