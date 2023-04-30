using CattleMgm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CattleMgm.Repository.Log
{
    public class LogRepository : ILogRepository
    {
        private praktikadbContext _context;

        public LogRepository(praktikadbContext context)
        {
            _context = context;
        }

        public List<Data.Entities.Log> GetLogs()
        {
            var logs = new List<Data.Entities.Log>();
            logs = _context.Log
                .ToList();

            return logs;
        }
    }
}
