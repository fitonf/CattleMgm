using Microsoft.AspNetCore.Mvc;

namespace CattleMgm.Controllers
{
    public class CattleController : Controller
    {
        public IActionResult Index()
        {
            var lista = new List<int>();
            for (int i = 0; i <= 10; i++)
            {
                lista.Add(i);
            }

            return View(lista);
        }

        public IActionResult Visari()
        {
            var lista = new List<int>();
            for(int i = 0; i<=10; i++)
            {
                lista.Add(i);  
            }

            return View("Index", lista);
        }
    }
}
