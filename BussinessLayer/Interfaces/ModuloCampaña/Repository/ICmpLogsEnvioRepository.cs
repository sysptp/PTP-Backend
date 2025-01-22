using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpLogsEnvioRepository
    {
        Task AddAsync(CmpLogsEnvio cmpLogsEnvio);
        Task<List<CmpLogsEnvio>> GetAllAsync(long empresaId);
        Task<CmpLogsEnvio> GetByIdAsync(int id);
    }
}
