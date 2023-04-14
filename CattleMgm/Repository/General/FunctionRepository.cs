using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.Helpers;
using CattleMgm.Models.Menu;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Repository.General
{
    public class FunctionRepository : IFunctionRepository
    {
        private ApplicationDbContext _context;
        private praktikadbContext _db;

        public FunctionRepository(ApplicationDbContext context, praktikadbContext db)
        {
            _context = context;
            _db = db;
        }
        public async Task<List<ListOfMenus>> GetListOfMenus(string Role, LanguageEnum lang)
        {
            return await _context.Set<ListOfMenus>().FromSqlInterpolated(sql: $"SELECT * FROM ListOfMenus({Role}, {lang})").ToListAsync();
        }
    }
}
