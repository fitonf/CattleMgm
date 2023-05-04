namespace CattleMgm.Models
{
    public class FarmModel
    {
        public int Id { get; set; }
        public string FarmName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public string Addres { get; set; } = string.Empty;

        public bool Notification { get; set; }
        public bool AdminNotification { get; set; }
        public string? ImageProfile { get; set; }
    }

    public class FarmRaportModel
    {
        public int Id { get; set; }
        public string FarmName { get; set; }
        public string Place { get; set; }
        public string Addres { get; set; }
    }
}
