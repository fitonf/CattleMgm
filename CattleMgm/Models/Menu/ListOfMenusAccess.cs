namespace CattleMgm.Models.Menu
{
    public class ListOfMenusAccess
    {
        public int MenuId { get; set; }

        public int submenu { get; set; }

        public string? MenuName { get; set; }

        public string? SubmenuName { get; set; }

        public string? Icon { get; set; }

        public bool HasSub { get; set; }

        public bool HasAccess { get; set; }

        public string? policy { get; set; }
    }
}
