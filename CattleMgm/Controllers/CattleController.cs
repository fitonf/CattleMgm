using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Controllers
{
    
    public class CattleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private praktikadbContext _db;
        public CattleController(ApplicationDbContext context, praktikadbContext db)
        {
            _context = context;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            CallProcedure obj = new CallProcedure(_context);

            var list = obj.cattleList();

            var listaGjedheve = await _db.Cattle.Select(q => new ProcedureModel{ Gender = q.Gender, Name = q.Name}).ToListAsync();
            
            //List<ProcedureModel> model = new List<ProcedureModel>();

            //foreach (var item in list)
            //{
            //    model.Add(new ProcedureModel
            //    {
            //        Name = item.Name,
            //        Gender = item.Gender
            //    });
            //}

            return View(listaGjedheve);
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
