using System;
using System.Collections.Generic;

namespace CattleMgmApi.Data.Entities;

public partial class Municipality
{
    public int Id { get; set; }

    public string Emri { get; set; } = null!;

    public int? Zip { get; set; }

    public virtual ICollection<Cattle> Cattle { get; set; } = new List<Cattle>();
}
