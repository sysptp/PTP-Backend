
using BussinessLayer.DTOs.Account;

namespace BussinessLayer.Interfaces.Services.IOtros
{
    public interface ITokenVerificationStrategy
    {
        Task<ExternalUserInfo> VerifyTokenAsync(string token);
    }
}
