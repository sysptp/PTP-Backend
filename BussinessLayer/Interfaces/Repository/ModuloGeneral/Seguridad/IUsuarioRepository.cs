using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        bool EmailExists(string email);
    }
}
