using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña
{
    public interface ICmpClienteRepository
    {
        Task<IEnumerable<CmpCliente>> GetAllAsync(long empresaId);
        Task<CmpCliente?> GetByIdAsync(int id,long empresaId);
        Task AddAsync(CmpCliente cliente);
        Task UpdateAsync(CmpCliente cliente);
        Task DeleteAsync(long id);
    }
}
