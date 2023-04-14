using CattleMgm.Data;
using CattleMgm.Models;
using CattleMgm.Models.Menu;
using CattleMgm.Repository.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CattleMgm.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private IFunctionRepository _functionRepository;
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public MenuViewComponent(IFunctionRepository functionRepository,
            ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _functionRepository = functionRepository;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ListOfMenus> menusTemp = new List<ListOfMenus>();

            Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(UserClaimsPrincipal);

            ApplicationUser user = await GetCurrentUserAsync();

            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var item in roles)
            {
                var role = await _roleManager.FindByNameAsync(item);
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                var rolemenus = (await _functionRepository.GetListOfMenus(item, user.Language)).Where(t => !menusTemp.Where(m => m.SubMenuController == t.SubMenuController).Any()).ToList();
                menusTemp.AddRange(rolemenus);
            }

            List<MenuViewModel> menus = menusTemp.OrderBy(t => t.OrdinalNumberM).Select(t => new
            {
                Action = t.MenuAction,
                Area = t.MenuArea,
                Controller = t.MenuController,
                HasSub = t.HasSub,
                Icon = t.Icon,
                Name = t.MenuName,
                MenuId = t.MenuId,
                MenuStaysOpenFor = t.MenuStaysOpenFor
            }).Distinct().ToList()
                  .Select(t => new MenuViewModel
                  {
                      Action = t.Action,
                      Area = t.Area,
                      Controller = t.Controller,
                      HasSubMenu = t.HasSub,
                      Name = t.Name,
                      Icon = t.Icon,
                      StaysOpenFor = t.MenuStaysOpenFor,
                      Submenus = menusTemp.Where(s => s.MenuId == t.MenuId).OrderBy(t => t.OrdinalNumberS).Select(s => new SubmenuViewModel
                      {
                          SubMenuID = s.SubMenuId,
                          Action = s.SubMenuAction,
                          Area = s.SubMenuArea,
                          Controller = s.SubMenuController,
                          Name = s.SubMenuName,
                          Icon = s.SubIcon,
                          IsBlazor = s.IsBlazor,
                          ParentId = s.ParentId,
                          StaysOpenFor = s.SubMenuStaysOpenFor
                      }).Distinct().ToList()
                  }).ToList();

            return await Task.FromResult((IViewComponentResult)View("Sidebar", menus));
        }
    }
}
