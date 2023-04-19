using System;
using System.Collections.Generic;

namespace CattleMgm.Data.Entities
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            Farmer = new HashSet<Farmer>();
            MenuInsertedFromNavigation = new HashSet<Menu>();
            MenuUpdatedFromNavigation = new HashSet<Menu>();
            SubMenuInsertedFromNavigation = new HashSet<SubMenu>();
            SubMenuUpdatedFromNavigation = new HashSet<SubMenu>();
            Role = new HashSet<AspNetRoles>();
        }

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

        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual ICollection<Farmer> Farmer { get; set; }
        public virtual ICollection<Menu> MenuInsertedFromNavigation { get; set; }
        public virtual ICollection<Menu> MenuUpdatedFromNavigation { get; set; }
        public virtual ICollection<SubMenu> SubMenuInsertedFromNavigation { get; set; }
        public virtual ICollection<SubMenu> SubMenuUpdatedFromNavigation { get; set; }

        public virtual ICollection<AspNetRoles> Role { get; set; }
    }
}
