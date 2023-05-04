using CattleMgm.Controllers;
using CattleMgm.Data.Entities;
using CattleMgm.Repository.Cattles;
using Microsoft.EntityFrameworkCore;
using CattleMgm.Repository.Humidity;
using CattleMgm.Repository.CattleTemperature;

namespace CattleMgm.Repository.Humidity
{
    public class HumidityRepository : IHumidityRepository
    {
     
            private praktikadbContext _context;

            public HumidityRepository(praktikadbContext context)
            {
                _context = context;
            }

            public async Task<List<Data.Entities.CattleHumidity>> GetHumidity()
            {
                var hum = new List<Data.Entities.CattleHumidity>();
                
                hum = await _context.CattleHumidity
                 .Include(x => x.Cattle)
                 .ToListAsync();
              

                return hum;


            }
        }
    }