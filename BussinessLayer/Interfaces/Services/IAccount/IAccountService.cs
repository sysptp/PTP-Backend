using BussinessLayer.DTOs.Account;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account;
using Microsoft.AspNetCore.Identity;

namespace BussinessLayer.Interfaces.Services.IAccount
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> EnableTwoFactorAuthenticationAsync(string userId);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterExternalUserAsync(ExternalLoginInfo info, string origin);
        //Task<RegisterResponse> RegisterExternalUserAsync(ExternalRegisterRequest request);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<string> SendConfirmationEmailAsync(string email, string origin);
        Task<string> SendPasswordResetEmailAsync(string email, string origin);
        Task<bool> VerifyUser(string UserName);
        Task<bool> VerifyUserById(int userId);
        Task<AuthenticationResponse> VerifyTwoFactorCodeAsync(string userId, string code);
        Task<List<string>> ValidateUserRegistrationAsync(string userName, string email);
    }
}