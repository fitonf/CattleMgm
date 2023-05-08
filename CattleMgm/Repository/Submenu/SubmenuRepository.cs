using CattleMgm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Repository.Submenu
{
    public class SubmenuRepository:ISubmenuRepository
    {
        private praktikadbContext _context;

        public SubmenuRepository(praktikadbContext context)
        {
            _context = context;
        }

        public async Task<List<Data.Entities.SubMenu>> GetSubMenus()
        {
            var submenu = new List<Data.Entities.SubMenu>();
           
            submenu = await _context.SubMenu
             .Include(x => x.Menu)
             .ToListAsync();
       
            return submenu;


        }
    }
}
