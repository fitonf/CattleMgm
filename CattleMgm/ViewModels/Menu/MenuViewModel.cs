namespace CattleMgm.ViewModels.Menu
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }    

        public string Area { get; set; }

        public bool HasSubMenu { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }

        public string Icon { get; set; }
    }

    public class MenuCreateViewModel
    {
        public string Name_SQ { get; set; }

        public string Name_EN { get; set; }

        public string Name_SR { get; set; }   
        
        public string Icon { get; set; }

        public string? Action { get; set; }

        public string? Controller { get; set; }

        public bool HasSubMenu { get; set; }

        public string? Area { get; set; }

        public string? Policy { get; set; }

        public string? StaysOpenFor { get; set; }

        public int OrdinalNumber { get; set; }
    }
}
