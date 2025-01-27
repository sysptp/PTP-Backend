using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpConfiguracionesSmtpRepository
    {
        Task<CmpConfiguracionesSmtp> GetyByIdAsync(int id, int empresaId);
        Task<List<CmpConfiguracionesSmtp>> GetAllAsync(int empresaId);
        Task AddAsync(CmpConfiguracionesSmtp configuracionesSmtp);
        Task DeleteAsync(int id, int empresaId, string usuarioModificacion);
        Task UpdateAsync(CmpConfiguracionesSmtp configuracionesSmtp);
    }
}
