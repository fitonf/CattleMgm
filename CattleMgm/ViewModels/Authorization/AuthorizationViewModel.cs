using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.Authorization
{
    public class AuthorizationViewModel
    {
    }

    public class AuthorizationCreateViewModel
    {
        [Display(Name = "Role")]
        [Required]
        public string Role { get; set; }
    }
}
