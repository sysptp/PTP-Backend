using BussinessLayer.DTOs.Configuracion.Account;
using BussinessLayer.DTOs.Configuracion.Seguridad.Usuario;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface IUsuarioService : IGenericService<RegisterRequest, UserResponse, Usuario>
    {
        Task<List<UserResponse>> GetAllWithFilters(long? companyId, long? sucursalId, int? roleId,bool? areActive);
        Task UpdateUser(UpdateUserRequest request);
    }
}
