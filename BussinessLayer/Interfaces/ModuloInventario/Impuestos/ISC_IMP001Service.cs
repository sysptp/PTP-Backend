using DataLayer.Models.ModuloInventario.Impuesto;

namespace BussinessLayer.Interfaces.ModuloInventario.Impuestos
{
    public interface ISC_IMP001Service
    {
        Task Add(SC_IMP001 model);

        Task Delete(SC_IMP001 model);

        Task<List<SC_IMP001>> GetAll();

        Task<List<SC_IMP001>> GetAllIndex();

        Task<SC_IMP001> GetById(int id);

        Task Update(SC_IMP001 model);
    }
}
