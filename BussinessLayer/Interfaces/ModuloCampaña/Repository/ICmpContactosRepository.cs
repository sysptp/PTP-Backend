using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpContactosRepository
    {
        Task<IEnumerable<CmpContactos>> GetAllAsync(long empresaId);
        Task<CmpContactos?> GetByIdAsync(long id, long empresaId);
        Task AddAsync(CmpContactos contacto);
        Task UpdateAsync(CmpContactos contacto);
        Task DeleteAsync(long id);
    }
}
