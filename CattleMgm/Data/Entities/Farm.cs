using System;
using System.Collections.Generic;

namespace CattleMgm.Data.Entities
{
    public partial class Farm
    {
        public Farm()
        {
            Cattle = new HashSet<Cattle>();
        }

        public int Id { get; set; }
        public int FarmerId { get; set; }
        public string? Name { get; set; }
        public string? Place { get; set; }
        public string? Address { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? LastUpdated { get; set; }
        public string? LastUpdatedBy { get; set; }

        public virtual Farmer Farmer { get; set; } = null!;
        public virtual ICollection<Cattle> Cattle { get; set; }
    }
}
