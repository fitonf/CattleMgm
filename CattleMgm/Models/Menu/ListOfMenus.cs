namespace CattleMgm.Models.Menu
{
    public class MenuViewModel
    {
        public string Name { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public bool HasSubMenu { get; set; }

        public string Icon { get; set; }

        public string StaysOpenFor { get; set; }

        public List<SubmenuViewModel> Submenus { get; set; }
    }

    public class SubmenuViewModel
    {
        public int? SubMenuID { get; set; }

        public string Name { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Icon { get; set; }

        public bool IsBlazor { get; set; }

        public int? ParentId { get; set; }

        public string StaysOpenFor { get; set; }
    }

    public class ListOfMenus
    {
        public int MenuId { get; set; }

        public string? MenuName { get; set; }

        public string? MenuArea { get; set; }

        public string? MenuController { get; set; }

        public string? MenuAction { get; set; }

        public string? MenuStaysOpenFor { get; set; }

        public int? SubMenuId { get; set; }

        public string? SubMenuName { get; set; }

        public string? SubMenuArea { get; set; }

        public string? SubMenuController { get; set; }

        public string? SubMenuAction { get; set; }

        public string? SubMenuStaysOpenFor { get; set; }

        public string? Icon { get; set; }

        public string? SubIcon { get; set; }

        public bool HasSub { get; set; }

        public int OrdinalNumberM { get; set; }

        public int OrdinalNumberS { get; set; }

        public bool IsBlazor { get; set; }

        public int? ParentId { get; set; }
    }
}
