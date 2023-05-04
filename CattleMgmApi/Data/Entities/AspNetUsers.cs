using System;
using System.Collections.Generic;

namespace CattleMgmApi.Data.Entities;

public partial class AspNetUsers
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool? IsRoleConfirmed { get; set; }

    public string? RoleId { get; set; }

    public int Language { get; set; }

    public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; } = new List<AspNetUserClaims>();

    public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; } = new List<AspNetUserLogins>();

    public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; } = new List<AspNetUserTokens>();

    public virtual ICollection<Cattle> CattleCreatedByNavigation { get; set; } = new List<Cattle>();

    public virtual ICollection<Cattle> CattleLastUpdatedByNavigation { get; set; } = new List<Cattle>();

    public virtual ICollection<Farmer> Farmer { get; set; } = new List<Farmer>();

    public virtual ICollection<Menu> MenuInsertedFromNavigation { get; set; } = new List<Menu>();

    public virtual ICollection<Menu> MenuUpdatedFromNavigation { get; set; } = new List<Menu>();

    public virtual ICollection<SubMenu> SubMenuInsertedFromNavigation { get; set; } = new List<SubMenu>();

    public virtual ICollection<SubMenu> SubMenuUpdatedFromNavigation { get; set; } = new List<SubMenu>();

    public virtual ICollection<AspNetRoles> Role { get; set; } = new List<AspNetRoles>();
}
