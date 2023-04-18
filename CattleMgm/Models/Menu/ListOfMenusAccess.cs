using System.ComponentModel.DataAnnotations;

namespace CattleMgm.Models.Menu
{
    public class ListOfMenusAccess
    {
        public int MenuId { get; set; }

        public int submenu { get; set; }
        [Display(Name = "Emri i menus")]
        public string? MenuName { get; set; }

        public string? SubmenuName { get; set; }

        public string? Icon { get; set; }

        public bool HasSub { get; set; }

        public bool HasAccess { get; set; }

        [Display(Name = "Protokoli")]
        public string? policy { get; set; }
    }
}
