using DataLayer.Models;
using DataLayer.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface ISC_HORA_X_USR002Service
    {
        Task Add(SC_HORA_X_USR002 model);

        Task Delete(SC_HORA_X_USR002 model);

        Task<List<SC_HORA_X_USR002>> GetAll();

        Task<List<SC_HORA_X_USR002>> GetAllIndex();

        Task<SC_HORA_X_USR002> GetById(int id);

        Task Update(SC_HORA_X_USR002 model);
    }
}
