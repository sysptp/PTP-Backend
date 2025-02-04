
using BussinessLayer.DTOs.Account;
using BussinessLayer.Interfaces.Services.IOtros;

namespace BussinessLayer.Services.Otros.AutenticationStrategy
{
    public class FacebookTokenVerificationStrategy : ITokenVerificationStrategy
    {
        public Task<ExternalUserInfo> VerifyTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
