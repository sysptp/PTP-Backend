using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpAgendarCampanaRepository
    {
        Task Add(CmpAgendarCampana campanaDetalle);
        Task Update(CmpAgendarCampana campanaDetalle);
        Task Delete(int id);
        Task<CmpAgendarCampana> GetById(int id);
        Task<List<CmpAgendarCampana>> GetAll(int empresaId);
    }
}
