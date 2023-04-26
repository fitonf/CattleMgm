namespace CattleMgm.ViewModels.Milk
{
    public class MilkViewModel
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

    public class MilkCreateViewModel
    {
        public int CattleId { get; set; }
        public double Price { get; set; }

        public double LitersCollected { get; set; }
    }
    public class MilkEditViewModel :MilkCreateViewModel
    {
       public int Id { get; set; }
    }
}
