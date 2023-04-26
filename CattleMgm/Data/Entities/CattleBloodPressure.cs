using System;
using System.Collections.Generic;

namespace CattleMgm.Data.Entities
{
    public partial class CattleBloodPressure
    {
        public int Id { get; set; }
        public int CattleId { get; set; }
        public int SystolicValue { get; set; }
        public int DiastolicValue { get; set; }
        public DateTime DateMeasured { get; set; }
        public string CreatedBy { get; set; } = null!;

        public virtual Cattle Cattle { get; set; } = null!;
    }
}
