using DataLayer.Models.ModuloGeneral.Geografia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Geografia
{
    public interface ICiudades_X_PaisesService
    {

        Task Add(Ciudades_X_Paises model);

        Task Delete(Ciudades_X_Paises model);

        Task<List<Ciudades_X_Paises>> GetAll();

        Task<Ciudades_X_Paises> GetById(int id);

        Task Update(Ciudades_X_Paises model);
    }
}
