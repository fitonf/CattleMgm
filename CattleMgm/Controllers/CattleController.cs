using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Models;
using CattleMgm.Repository.Cattles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Controllers
{
    [AllowAnonymous]
    public class CattleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private praktikadbContext _db;
        private ICattleRepository _cattleRepository;

        public CattleController(ApplicationDbContext context, praktikadbContext db, 
                                ICattleRepository cattleRepository)
        {
            _context = context;
            _db = db;
            _cattleRepository = cattleRepository;
        }
        public async Task<IActionResult> Index()
        {
            var lista = _cattleRepository.GetCattles();

            CallProcedure obj = new CallProcedure(_context);

            var list = obj.cattleList();

            var listaGjedheve = await _db.Cattle.Select(q => new ProcedureModel{ Gender = q.Gender, Name = q.Name}).ToListAsync();

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
