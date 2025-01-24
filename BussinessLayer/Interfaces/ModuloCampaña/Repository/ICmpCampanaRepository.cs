using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpCampanaRepository
    {
        Task<CmpCampana> GetByIdAsync(int id, int empresaId);
        Task<List<CmpCampana>> GetAllAsync(int empresaId);
        Task CreateAsync(CmpCampana campana);
        Task DeleteAsync(int id);
        Task UpdateAsync(CmpCampana campana);
        Task UpdateStatus(int id, int status);
    }
}
