using System;
using System.Collections.Generic;

namespace CattleMgmApi.Data.Entities;

public partial class Farmer
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public string? PersonalNumber { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public DateTime Created { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUpdated { get; set; }

    public string? LastUpdatedBy { get; set; }

    public virtual ICollection<Farm> Farm { get; set; } = new List<Farm>();

    public virtual AspNetUsers? User { get; set; }
}
