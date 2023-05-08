namespace CattleMgm.ViewModels.Home
{
    public class BarChart
    {
        public BarChart()
        {
            Data = new double[7];
        }

        public string[] Days { get; set; }
        public double[] Data { get; set; }
    }

    public class Days
    {
        public string Day { get; set; } = string.Empty;
    }

    public class Data
    {
        //public string Day { get; set; }

        public double[] LitersCollected { get; set; }
    }
}
