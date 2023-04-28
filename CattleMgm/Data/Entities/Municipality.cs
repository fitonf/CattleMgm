using System;
using System.Collections.Generic;

namespace CattleMgm.Data.Entities
{
    public partial class Municipality
    {
        public Municipality()
        {
            Cattle = new HashSet<Cattle>();
        }

        public int Id { get; set; }
        public string Emri { get; set; } = null!;
        public int? Zip { get; set; }

        public virtual ICollection<Cattle> Cattle { get; set; }
    }
}
