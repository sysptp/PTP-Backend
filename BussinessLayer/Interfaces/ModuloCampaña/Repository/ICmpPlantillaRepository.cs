using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpPlantillaRepository
    {
        Task<CmpPlantillas> GetByIdAsync(int id, long empresaId);
        Task<List<CmpPlantillas>> GetAllAsync(long empresaId);
        Task AddAsync(CmpPlantillas plantilla);
        Task DeleteAsync(int id);
        Task UpdateAsync(CmpPlantillas plantilla);
    }
}
