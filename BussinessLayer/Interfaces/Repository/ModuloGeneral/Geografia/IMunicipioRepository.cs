using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia
{
    public interface IMunicipioRepository : IGenericRepository<Municipio>
    {
        IEnumerable<Municipio> GetMunicipiosByProvinciaId(int provinciaId);
        Task<List<Municipio>> GetIndex();
    }
}
