using DataLayer.Models;
using DataLayer.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface ISC_HORAGROUP002Service
    {
        Task Add(SC_HORAGROUP002 model);

        Task Delete(SC_HORAGROUP002 model);

        Task<List<SC_HORAGROUP002>> GetAll();

        Task<List<SC_HORAGROUP002>> GetAllIndex();

        Task<SC_HORAGROUP002> GetById(int id);

        Task Update(SC_HORAGROUP002 model);
    }
}
