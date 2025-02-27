using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetClaimValue(string claimType)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return null;

        var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;

        return jwtToken?.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;
    }
}
