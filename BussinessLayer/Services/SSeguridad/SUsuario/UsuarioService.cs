using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Account;
using BussinessLayer.DTOs.Configuracion.Seguridad.Usuario;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Services.SSeguridad.SUsuario
{
    public class UsuarioService : GenericService<RegisterRequest, UserResponse, Usuario>, IUsuarioService
    {
        public UsuarioService(IGenericRepository<Usuario> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
