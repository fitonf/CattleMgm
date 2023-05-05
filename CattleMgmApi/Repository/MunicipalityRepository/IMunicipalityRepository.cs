using CattleMgmApi.Data.Entities;


namespace CattleMgmApi.Repository
{
    public interface IMunicipalityRepository
    {
        Task SaveChanges();

        Task<Municipality?> GetMunicipalityById(int id);

        Task<IEnumerable<Municipality>> GetAllMunicipality();

        Task CreateMunicipality(Municipality municipality);

        void UpdateMunicipality(Municipality municipality,int Id);

        
    }
}