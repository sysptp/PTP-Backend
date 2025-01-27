using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpTipoContactoRepository
    {
        Task<IEnumerable<CmpTipoContacto>> GetAllAsync(int idEmpresa);
        Task<CmpTipoContacto?> GetByIdAsync(int id, int idEmpresa);
        Task AddAsync(CmpTipoContacto tipoContacto);
        Task UpdateAsync(CmpTipoContacto tipoContacto);
        Task DeleteAsync(int id);
    }
}
