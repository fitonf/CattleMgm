using System;
using System.Collections.Generic;

namespace CattleMgmApi.Data.Entities;

public partial class CattlePosition
{
    public int Id { get; set; }

    public int CattleId { get; set; }

    public double Lat { get; set; }

    public double Long { get; set; }

    public DateTime DateMeasured { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual Cattle Cattle { get; set; } = null!;
}
