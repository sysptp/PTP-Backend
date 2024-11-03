
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Interfaces.Repository.Geografia
{
    public interface IMunicipioRepository : IGenericRepository<Municipio>
    {
        IEnumerable<Municipio> GetMunicipiosByProvinciaId(int provinciaId);
        Task<List<Municipio>> GetIndex();
    }
}
