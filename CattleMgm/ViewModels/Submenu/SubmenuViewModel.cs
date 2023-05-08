using System.ComponentModel.DataAnnotations;

namespace CattleMgm.ViewModels.Submenu
{
    public class SubmenuViewModel
    {
        public int MId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Area { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }
    }

    public class SubmenuCreateViewModel
    {
        public int MId { get; set; }

        [Display(Name = "Parent submenu")]
        public int? ParentID { get; set; }

        public string? Menu { get; set; }

        [Display(Name = "Name albanian")]
        [Required]
        public string NameSq { get; set; }

        [Display(Name = "Name english")]
        [Required]
        public string NameEn { get; set; }

        [Display(Name = "Name serbian")]
        [Required]
        public string NameSr { get; set; }

        [Display(Name = "Area")]
        public string? Area { get; set; }

        [Display(Name = "Controller")]
        [Required]
        public string Controller { get; set; }

        [Display(Name = "Action")]
        [Required]
        public string Action { get; set; }

        [Display(Name = "OrdinalNumber")]
        public int OrdinalNumber { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "Policy")]
        [Required]
        public string Policy { get; set; }

        [Display(Name = "Icon")]
        [Required]
        public string Icon { get; set; }

        [Display(Name = "SubStaysOpenFor")]
        [Required]
        public string StaysOpenFor { get; set; }
    }
    public class SubmenuEditViewModel : SubmenuCreateViewModel
    {
        public int Id { get; set; }
    }


    public class SubMenuReportModel
    {
        public int Id { get; set; }
        public int MId { get; set; }
        public int? ParentID { get; set; }

        public string NameSq { get; set; }  
        public string NameEn { get; set; }

        public string NameSr { get; set; }
        public string? Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int OrdinalNumber { get; set; }
        public bool IsActive { get; set; }
        public string Policy { get; set; }
        public string Icon { get; set; }
        public string StaysOpenFor { get; set; }
    }

    public class SearchSubmenu
    {
        public int MId { get; set; }
        public string? Menu { get; set; }

        public string? Name { get; set; }

   
        public string? Area { get; set; }

        public string? Action { get; set; }

        public string? Controller { get; set; }

    }
}
