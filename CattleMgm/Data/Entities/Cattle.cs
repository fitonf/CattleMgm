using System;
using System.Collections.Generic;

namespace CattleMgm.Data.Entities
{
    public partial class Cattle
    {
        public int Id { get; set; }
        public Guid UniqueIdentifier { get; set; }
        public int FarmId { get; set; }
        public int BreedId { get; set; }
        public string? Name { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public double Weight { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? LastUpdatedBy { get; set; }

        public virtual Breed Breed { get; set; } = null!;
        public virtual Farm Farm { get; set; } = null!;
    }
}
