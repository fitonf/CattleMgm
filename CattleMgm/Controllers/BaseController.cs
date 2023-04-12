using CattleMgm.Data;
using CattleMgm.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CattleMgm.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly praktikadbContext _db;

        public BaseController(ApplicationDbContext context, praktikadbContext db)
        {
            _context = context;
            _db = db;
        }
    }
}
