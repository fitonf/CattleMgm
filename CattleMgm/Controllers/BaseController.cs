using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CattleMgm.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
