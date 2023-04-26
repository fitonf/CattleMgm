using System;
using System.Collections.Generic;

namespace CattleMgm.Data.Entities
{
    public partial class CattleHumidity
    {
        public int Id { get; set; }
        public int CattleId { get; set; }
        public double Humidity { get; set; }
        public DateTime DateMeasured { get; set; }
        public string CreatedBy { get; set; } = null!;
    
        public virtual Cattle Cattle { get; set; } = null!;
    }
}
