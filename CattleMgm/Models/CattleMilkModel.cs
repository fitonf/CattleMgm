namespace CattleMgm.Models
{
    public class CattleMilkModel
    {
        public int CattleId { get; set; }

        public int Id { get; set; }
        public string Identifier { get; set; }

        public string CattleName { get; set; }

        public double LitersCollected { get; set; }

        public string DateCollected { get; set; }

        public double Price { get; set; }

        public double TotalProfit { get; set; }
    }
    public class CattleMilkReportModel {

        public int CattleId { get; set; }

        public int Id { get; set; }

        public string CattleName { get; set; } = null!;

        public double LitersCollected { get; set; }

        public string DateCollected { get; set; } = null!;

        public double Price { get; set; }

        public double TotalProfit { get; set; }
    }
}
