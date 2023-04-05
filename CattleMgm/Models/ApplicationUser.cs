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
    }

    public class ApplicationRole : IdentityRole
    {

    }
}
