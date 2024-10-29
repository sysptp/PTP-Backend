using DataLayer.Models;
using DataLayer.Models.Geografia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface IPaisService
    {
        Task Add(Pais model);

        Task Delete(Pais model);

        Task<List<Pais>> GetAll();

        Task<Pais> GetById(int id);

        Task Update(Pais model);

    }
}