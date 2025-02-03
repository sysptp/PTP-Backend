using BussinessLayer.DTOs.Account;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace BussinessLayer.Interfaces.Services.IAccount
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterUserAsync(BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account.RegisterRequest request, string origin);
        Task<bool> VerifyUser(string UserName);
        Task<bool> VerifyUserById(int userId);
        Task<string> SendConfirmationEmailAsync(string email, string origin);
        Task<string> EnableTwoFactorAuthenticationAsync (string userId);
        Task<RegisterResponse> RegisterExternalUserAsync(ExternalLoginInfo info, string origin);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario.ResetPasswordRequest request);
    }
}