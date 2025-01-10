using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña
{
    public interface ICmpContactosRepository
    {
        Task<IEnumerable<CmpContactos>> GetAllAsync(int empresaId);
        Task<CmpContactos?> GetByIdAsync(int id,int empresaId);
        Task AddAsync(CmpContactos contacto);
        Task UpdateAsync(CmpContactos contacto);
        Task DeleteAsync(int id);
    }
}
