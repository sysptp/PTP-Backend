using DataLayer.Models.ModuloCampaña;

namespace BussinessLayer.Interfaces.ModuloCampaña.Repository
{
    public interface ICmpFrecuenciaRepository
    {
        Task<CmpFrecuencia> GetById(int id);
        Task<List<CmpFrecuencia>> GetAll();
    }
}
