using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpPlantillaRepository
    {
        Task<CmpPlantillas> GetByIdAsync(int id, int empresaId);
        Task<List<CmpPlantillas>> GetAllAsync(int empresaId);
        Task AddAsync(CmpPlantillas plantilla);
        Task DeleteAsync(int id);
        Task UpdateAsync(CmpPlantillas plantilla);
    }
}
