using System.Security.Claims;

namespace BussinessLayer.Interfaces.IAutenticacion
{
    public interface IClaimsService
    {
        string GetClaimValueByType(string claimType);
    }
}
