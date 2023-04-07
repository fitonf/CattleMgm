using CattleMgm.Data;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Models
{
    public class CallProcedure
    {
        private ApplicationDbContext _context;

        public CallProcedure(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<ProcedureModel> cattleList() =>
         _context.Set<ProcedureModel>().FromSqlInterpolated(sql: $"EXEC [dbo].[selectCattle]").ToList();
    }
}
