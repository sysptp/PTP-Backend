using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BussinessLayer.Services.SAutenticacion
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IHttpContextAccessor httpContextAccessor, ILogger<TokenService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public string? GetClaimValue(string claimType)
        {
            try
            {
                // ⭐ VERIFICAR SI HAY CONTEXTO HTTP
                var context = _httpContextAccessor.HttpContext;
                if (context == null)
                {
                    _logger.LogWarning("No HttpContext available for token processing");
                    return null;
                }

                // ⭐ VERIFICAR SI ES ENDPOINT ANÓNIMO
                if (IsAnonymousEndpoint(context))
                {
                    return null; // No procesar token en endpoints anónimos
                }

                // Obtener token del header Authorization
                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return null;
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                // ⭐ VALIDAR TOKEN ANTES DE PROCESARLO
                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(token))
                {
                    _logger.LogWarning("Invalid JWT token format");
                    return null;
                }

                var jwtToken = handler.ReadJwtToken(token);
                var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);

                return claim?.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading claim {ClaimType} from token: {Message}", claimType, ex.Message);
                return null;
            }
        }

        private bool IsAnonymousEndpoint(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                return endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null ||
                       endpoint.Metadata.GetMetadata<IAllowAnonymous>() != null;
            }

            // Verificar rutas específicas
            var path = context.Request.Path.Value?.ToLower();
            if (!string.IsNullOrEmpty(path))
            {
                var anonymousPaths = new[]
                {
                    "/api/v1/ctabookingportal/public/",
                    "/health",
                    "/swagger"
                };

                return anonymousPaths.Any(ap => path.StartsWith(ap));
            }

            return false;
        }
    }
}