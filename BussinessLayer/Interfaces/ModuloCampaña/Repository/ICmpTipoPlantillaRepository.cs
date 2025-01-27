using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpTipoPlantillaRepository
    {
        Task<CmpTipoPlantilla> GetByIdAsync(int id,int empresaId);
        Task<List<CmpTipoPlantilla>> GetAllAsync(int empresaId);
        Task AddAsync(CmpTipoPlantilla plantilla);
        Task DeleteAsync(int id);
        Task UpdateAsync(CmpTipoPlantilla plantilla);
    }
}
