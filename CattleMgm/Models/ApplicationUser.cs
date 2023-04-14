using CattleMgm.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CattleMgm.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(450)]
        public string? RoleId { get; set; }

        public bool? IsRoleConfirmed { get; set; }

        public LanguageEnum Language { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {

    }
}
