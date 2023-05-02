using System;
using System.Collections.Generic;

namespace CattleMgmApi.Data.Entities;

public partial class Farm
{
    public int Id { get; set; }

    public int FarmerId { get; set; }

    public string? Name { get; set; }

    public string? Place { get; set; }

    public string? Address { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdated { get; set; }

    public string? LastUpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Cattle> Cattle { get; set; } = new List<Cattle>();

    public virtual Farmer Farmer { get; set; } = null!;
}
