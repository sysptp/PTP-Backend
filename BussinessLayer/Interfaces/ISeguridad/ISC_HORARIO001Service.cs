using DataLayer.Models.Seguridad;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface ISC_HORARIO001Service
    {
        Task Add(SC_HORARIO001 model);

        Task Delete(SC_HORARIO001 model);

        Task<List<SC_HORARIO001>> GetAll();

        Task<List<SC_HORARIO001>> GetAllIndex();

        Task<SC_HORARIO001> GetById(int id);

        Task Update(SC_HORARIO001 model);
    }
}
