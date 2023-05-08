namespace CattleMgm.Repository.Submenu
{
    public interface ISubmenuRepository
    {

        Task<List<Data.Entities.SubMenu>> GetSubMenus();
    }
}
