using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.Roles
{
    public class RolesViewModel
    {
        
        //aaaaaaa
        [Required]
        [Display(Name = "Emri")]
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class RolesCreateViewModel
    {
        [Required]
        [Display(Name = "Emri")]
        public string Name { get; set; }
    }

    public class RolesEditViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
