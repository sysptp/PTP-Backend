using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpEstadoRepository
    {
        Task<List<CmpEstado>> GetAllAsync();
        Task<CmpEstado> GetByIdAsync(int id);
    }
}
