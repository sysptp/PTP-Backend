using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace PTP_API.Middlewares
{
    public class AuditingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuditingMiddleware> _logger;

        // ⭐ SOLO INYECTAR SERVICIOS SINGLETON EN EL CONSTRUCTOR
        public AuditingMiddleware(RequestDelegate next, ILogger<AuditingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // ⭐ INYECTAR SERVICIOS SCOPED EN EL MÉTODO InvokeAsync
        public async Task InvokeAsync(HttpContext context, ITokenService tokenService)
        {
            try
            {
                // Verificar si el endpoint permite acceso anónimo
                if (IsAnonymousEndpoint(context))
                {
                    await _next(context);
                    return;
                }

                // Verificar si hay token antes de procesarlo
                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    await ProcessRequestWithoutUser(context);
                    return;
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();
                if (string.IsNullOrEmpty(token))
                {
                    await ProcessRequestWithoutUser(context);
                    return;
                }

                // Procesar con token válido usando el servicio inyectado
                await ProcessRequestWithUser(context, token, tokenService);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en AuditingMiddleware: {Message}", ex.Message);
                await _next(context);
            }
        }

        private bool IsAnonymousEndpoint(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var allowAnonymous = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>();
                if (allowAnonymous != null)
                {
                    return true;
                }

                var controllerAnonymous = endpoint.Metadata.GetMetadata<IAllowAnonymous>();
                if (controllerAnonymous != null)
                {
                    return true;
                }
            }

            // Verificar rutas específicas
            var path = context.Request.Path.Value?.ToLower();
            if (!string.IsNullOrEmpty(path))
            {
                var anonymousPaths = new[]
                {
                    "/api/v1/ctabookingportal/public/",
                    "/health",
                    "/swagger",
                    "/api/v1/auth/login",
                    "/api/v1/auth/register"
                };

                return anonymousPaths.Any(ap => path.StartsWith(ap));
            }

            return false;
        }

        private async Task ProcessRequestWithUser(HttpContext context, string token, ITokenService tokenService)
        {
            try
            {
                // ⭐ USAR EL SERVICIO INYECTADO EN EL MÉTODO
                var userId = GetClaimFromToken(token, "sub");
                var userEmail = GetClaimFromToken(token, "email");

                await LogRequestWithUser(context, userId, userEmail);
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error procesando token para auditoría, continuando sin usuario");
                await ProcessRequestWithoutUser(context);
            }
        }

        private async Task ProcessRequestWithoutUser(HttpContext context)
        {
            await LogRequestWithoutUser(context);
            await _next(context);
        }

        // ⭐ MÉTODO DIRECTO PARA LEER CLAIMS SIN DEPENDENCIAS
        private string? GetClaimFromToken(string token, string claimType)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(token))
                {
                    return null;
                }

                var jwtToken = handler.ReadJwtToken(token);
                var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);
                return claim?.Value;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error reading claim {ClaimType} from token", claimType);
                return null;
            }
        }

        private async Task LogRequestWithUser(HttpContext context, string? userId, string? userEmail)
        {
            var requestInfo = new
            {
                Timestamp = DateTime.UtcNow,
                Method = context.Request.Method,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                UserId = userId ?? "Unknown",
                UserEmail = userEmail ?? "Unknown",
                IPAddress = GetClientIpAddress(context),
                UserAgent = context.Request.Headers["User-Agent"].FirstOrDefault()
            };

            _logger.LogInformation("Authenticated Request: {@RequestInfo}", requestInfo);
        }

        private async Task LogRequestWithoutUser(HttpContext context)
        {
            var requestInfo = new
            {
                Timestamp = DateTime.UtcNow,
                Method = context.Request.Method,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                Type = "Anonymous",
                IPAddress = GetClientIpAddress(context),
                UserAgent = context.Request.Headers["User-Agent"].FirstOrDefault()
            };

            _logger.LogInformation("Anonymous Request: {@RequestInfo}", requestInfo);
        }

        private string GetClientIpAddress(HttpContext context)
        {
            var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            }
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = context.Connection.RemoteIpAddress?.ToString();
            }
            return ipAddress ?? "Unknown";
        }
    }
}
