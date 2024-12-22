using DataLayer.Models.Otros;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.Services.IOtros
{
    public interface ITipoIdentificacionService
    {
        Task Add(Tipo_Identificacion model);

        Task Delete(Tipo_Identificacion model);

        Task<List<Tipo_Identificacion>> GetAll();

        Task<Tipo_Identificacion> GetById(int id);

        Task Update(Tipo_Identificacion model);
    }
}
