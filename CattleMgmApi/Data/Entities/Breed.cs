using System;
using System.Collections.Generic;

namespace CattleMgmApi.Data.Entities;

public partial class Breed
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Type { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdated { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual ICollection<Cattle> Cattle { get; set; } = new List<Cattle>();
}
