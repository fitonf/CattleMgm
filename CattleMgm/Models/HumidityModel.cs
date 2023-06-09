﻿namespace CattleMgm.Models
{
    public class HumidityReportModel
    {
        public int Id { get; set; }
        public string CattleId { get; set; }

        public double Humidity { get; set; }
        public DateTime DateMeasured { get; set; }
        public string CreatedBy { get; set; }

    }
    public class SearchHumidity
    {

        public int? CattleId { get; set; }
        public double? Humidity { get; set; }
        public string? DateMeasured { get; set; }
        public string? CreatedBy { get; set;}
    }
}
