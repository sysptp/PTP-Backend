using System.Security.Claims;

namespace BussinessLayer.Interfaces.Services.IAutenticacion
{
    public interface IClaimsService
    {
        string GetClaimValueByType(string claimType);
    }
}
