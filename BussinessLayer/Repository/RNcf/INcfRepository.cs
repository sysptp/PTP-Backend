using DataLayer.Models.Ncf;

namespace BussinessLayer.Repository.RNcf
{
    public interface INcfRepository
    {
        Task<bool> CreateNcfAsync(Ncf ncf);
        Task<bool> DeleteNcfAsync(string ncfType, int bussinesId);
        Task<Ncf> GetNcfByIdAsync(string ncfType, int bussinesId);
        Task<List<Ncf>> GetAllNcfsAsync(int bussinesId);
    }
}
