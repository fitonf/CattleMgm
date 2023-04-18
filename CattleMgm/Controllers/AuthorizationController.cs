using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers;
using CattleMgm.Helpers.Security;
using CattleMgm.Models;
using CattleMgm.Models.Menu;
using CattleMgm.Repository.General;
using CattleMgm.ViewModels.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CattleMgm.Controllers
{
    public class AuthorizationController : BaseController
    {
        public RoleManager<ApplicationRole> _roleManager;
        private IFunctionRepository _functionRepository;
        public AuthorizationController(ApplicationDbContext context, praktikadbContext db, 
            RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IFunctionRepository functionRepository) : base(context, db, userManager)
        {
            _roleManager = roleManager;
            _functionRepository = functionRepository;
        }

        public IActionResult Index()
        {
            //po i nxjerrim rolet prej db
            //per shkak qe me i shfaq ne view
            var roles = _roleManager.Roles.ToList();

            //tipi selectlist perdoret per krijimin e dropdownlist ne view
            //Fusha Id perdoret si key per me marr vleren, ne kete rast "a2a2ab30-5c75-4652-9a6e-b18ec684c8aa"
            //Name perdoret per me e shfaq emrin e opsionit ne dropdownlist
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View();
        }

        //metoda per me i shfaq menut
        //si parameter vjen roli i zgjedhur ne view index
        [HttpPost]
        public async Task<IActionResult> _Search(AuthorizationCreateViewModel model)
        {
            List<ListOfMenusAccess> lista = await _functionRepository.ListOfMenusAuthorized(model.Role, Helpers.LanguageEnum.Albanian);


            return PartialView(lista);
        }

        //Metoda perdoret per insertimin e qasjeve ne db
        //Nese ka qasje dhe access = false, i fshin qasjet
        //Nese nuk ka qasje dhe access = true i inserton
        [HttpPost]
        public async Task<IActionResult> ChangeAccess(string Role, string mide, string side, bool access)
        {
            if(string.IsNullOrEmpty(Role))
            {
                return Json(new ViewModels.ErrorViewModel 
                { ErrorNumber = ErrorStatus.Error, 
                  ErrorDescription = "Te dhenat jo valide" });
            }

            //Dekriptimin e id-ve te menus dhe submenus
            int menuId = AesCrypto.Decrypt<int>(mide);
            int submenuId = AesCrypto.Decrypt<int>(side);

            Menu menu = null;
            SubMenu subMenu = null;
            string claim = "", claimValue = "";

            //nese submenu ekziston
            if (submenuId != 0)
            {
                //gjejm submenu
                subMenu = await _db.SubMenu.FindAsync(submenuId);

                //marrim claims te submenus
                claim = subMenu.Claim.Split(':')[0];
                claimValue = subMenu.Claim.Split(':')[1];
            }
            else
            {
                //gjejm menu
                menu = await _db.Menu.FindAsync(menuId);

                //marrim claims te menus
                claim = menu.Claim.Split(':')[0];
                claimValue = menu.Claim.Split(':')[1];
            }

            //gjejm rolin ne baze te parametrit Role
            ApplicationRole role = await _roleManager.FindByIdAsync(Role);

            //bejme check ne db tek tabela AspNetRoleClaims
            //nese ky rol i permban keto claims
            var hasClaims = _db.AspNetRoleClaims.Where(q => q.RoleId == role.Id &&
                                                       q.ClaimType == claim &&
                                                       q.ClaimValue == claimValue).Any();
            //Nese parametri access vlera e te cilit 
            //vjen nga view
            //Nese true dhe role nuk ka claims
            //I ben insert ne tabelen AspNetRoleClaims
            if(access && !hasClaims)
            {
                await _roleManager.AddClaimAsync(role, new Claim(claim, claimValue));
            }
            else // e kunderta, i fshin claims nga tabela AspNetRoleClaims per rolin e zgjedhur
            {
                await _roleManager.RemoveClaimAsync(role, new Claim(claim, claimValue));
            }

            ViewModels.ErrorViewModel error = new ViewModels.ErrorViewModel
            {
                Title = "Sukses",
                ErrorNumber = ErrorStatus.Success,
                ErrorDescription = "Te dhenat u ruajten me sukses"
            };

            return Json(error);
        }
    }
}
