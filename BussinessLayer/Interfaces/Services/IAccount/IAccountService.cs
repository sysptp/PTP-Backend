using BussinessLayer.DTOs.Account;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario;

namespace BussinessLayer.Interfaces.Services.IAccount
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<bool> VerifyUser(string UserName);
        Task<bool> VerifyUserById(int userId);
    }
}