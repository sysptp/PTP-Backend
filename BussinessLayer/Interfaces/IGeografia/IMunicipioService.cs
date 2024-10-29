using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface IMunicipioService
    {
        IEnumerable<Municipio> GetMunicipiosByProvinciaId(int provinciaId);

        Task<List<Municipio>> GetIndex();

        Task Add(Municipio model);

        Task Delete(Municipio model);

        Task<List<Municipio>> GetAll();

        Task<Municipio> GetById(int id);

        Task Update(Municipio model);
    }
}