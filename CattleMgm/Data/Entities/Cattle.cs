using System;
using System.Collections.Generic;

namespace CattleMgm.Data.Entities
{
    public partial class Cattle
    {
        public Cattle()
        {
            CattleBloodPressure = new HashSet<CattleBloodPressure>();
            CattleHumidity = new HashSet<CattleHumidity>();
            CattleMilk = new HashSet<CattleMilk>();
            CattlePosition = new HashSet<CattlePosition>();
            CattleTemperature = new HashSet<CattleTemperature>();
        }

        public int Id { get; set; }
        public Guid UniqueIdentifier { get; set; }
        public int FarmId { get; set; }
        public int BreedId { get; set; }
        public int? MunicipalityId { get; set; }
        public string? Name { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public double Weight { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? LastUpdated { get; set; }
        public string? LastUpdatedBy { get; set; }

        public virtual Breed Breed { get; set; } = null!;
        public virtual AspNetUsers CreatedByNavigation { get; set; } = null!;
        public virtual Farm Farm { get; set; } = null!;
        public virtual AspNetUsers? LastUpdatedByNavigation { get; set; }
        public virtual Municipality? Municipality { get; set; }
        public virtual ICollection<CattleBloodPressure> CattleBloodPressure { get; set; }
        public virtual ICollection<CattleHumidity> CattleHumidity { get; set; }
        public virtual ICollection<CattleMilk> CattleMilk { get; set; }
        public virtual ICollection<CattlePosition> CattlePosition { get; set; }
        public virtual ICollection<CattleTemperature> CattleTemperature { get; set; }
    }
}
