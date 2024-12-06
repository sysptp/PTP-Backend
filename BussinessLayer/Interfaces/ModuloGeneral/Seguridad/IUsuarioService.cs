using BussinessLayer.DTOs.Account;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Interfaces.ModuloGeneral.Seguridad
{
    public interface IUsuarioService : IGenericService<RegisterRequest, UserResponse, Usuario>
    {
        Task<List<UserResponse>> GetAllWithFilters(long? companyId, long? sucursalId, int? roleId, bool? areActive);
        Task UpdateUser(UpdateUserRequest request);
        Task<UserResponse> GetByUserNameResponse(string userName);
    }
}
