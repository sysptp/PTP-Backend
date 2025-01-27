using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpCampanaDetalleRepository
    {
        Task Add(CmpCampanaDetalle campanaDetalle);
        Task Update(CmpCampanaDetalle campanaDetalle);
        Task Delete(int id);
        Task<CmpCampanaDetalle> GetById(int id);
        Task<List<CmpCampanaDetalle>> GetAll(int empresaId);
    }
}
