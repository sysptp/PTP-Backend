using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña
{
    public interface ICmpClienteRepository
    {
        Task<IEnumerable<CmpCliente>> GetAllAsync(int empresaId);
        Task<CmpCliente?> GetByIdAsync(int id,int empresaId);
        Task AddAsync(CmpCliente cliente);
        Task UpdateAsync(CmpCliente cliente);
        Task DeleteAsync(int id);
    }
}
