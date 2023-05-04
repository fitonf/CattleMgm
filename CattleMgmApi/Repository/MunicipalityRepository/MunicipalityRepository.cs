using CattleMgmApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CattleMgmApi.Repository.MunicipalityRepository
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        public PraktikadbContext _context;
        public MunicipalityRepository(PraktikadbContext context)
        {
            _context = context;
        }
        public async Task CreateMunicipality(Municipality municipality)
        {
            if (municipality == null)
            {
                throw new ArgumentNullException(nameof(municipality));
            }
            //insertimi ne databaze me EFCore

            await _context.Municipality.AddAsync(municipality);
        
    }

        public void DeleteMunicipality(Municipality municipality)
        {
            if(municipality == null)
                throw new NotImplementedException(nameof(municipality));

            _context.Municipality.Remove(municipality);
        }

        public async Task<IEnumerable<Municipality>> GetAllMunicipality()
        {
            var municipality = await _context.Municipality.ToListAsync();

            return municipality;
        }

        public async Task<Municipality?> GetMunicipalityById(int id)
        {
            return await _context.Municipality.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateMunicipality(Municipality municipality,int id)
        {
            if (municipality == null)
            {
                throw new NotImplementedException(nameof(municipality));
            }
            municipality.Id = id;

            _context.Municipality.Update(municipality);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
