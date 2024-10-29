using DataLayer.Models;
using DataLayer.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface IGn_PerfilService
    {
        Task Add(Gn_Perfil model);

        Task Delete(Gn_Perfil model);

        Task<List<Gn_Perfil>> GetAll();

        Task<Gn_Perfil> GetById(int id);

        Task Update(Gn_Perfil model);
    }
}
