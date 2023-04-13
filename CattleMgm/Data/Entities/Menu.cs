using System;
using System.Collections.Generic;

namespace CattleMgm.Data.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            SubMenu = new HashSet<SubMenu>();
        }

        public int MenuId { get; set; }
        public string NameSq { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string NameSr { get; set; } = null!;
        public bool HasSubMenu { get; set; }
        public string? Claim { get; set; }
        public string? ClaimType { get; set; }
        public string? Icon { get; set; }
        public bool IsActive { get; set; }
        public string? Area { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public int OrdinalNumber { get; set; }
        public string? Roles { get; set; }
        public string? StaysOpenFor { get; set; }
        public string InsertedFrom { get; set; } = null!;
        public DateTime InsertedDate { get; set; }
        public string? UpdatedFrom { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual AspNetUsers InsertedFromNavigation { get; set; } = null!;
        public virtual AspNetUsers? UpdatedFromNavigation { get; set; }
        public virtual ICollection<SubMenu> SubMenu { get; set; }
    }
}
