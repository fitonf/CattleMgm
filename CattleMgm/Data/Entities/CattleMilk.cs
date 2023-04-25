using System;
using System.Collections.Generic;

namespace CattleMgm.Data.Entities
{
    public partial class CattleMilk
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
        public int CattleId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; } = null!;
        public double LitersCollected { get; set; }
        public double Price { get; set; }

        public virtual Cattle Cattle { get; set; } = null!;
    }
}
