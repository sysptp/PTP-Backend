using Microsoft.AspNetCore.Authorization;
using PTP_API.Attributes;

namespace PTP_API.Middlewares
{
    public class ImprovedAuditingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;
        private readonly ILogger<ImprovedAuditingMiddleware> _logger;

        public ImprovedAuditingMiddleware(RequestDelegate next, ITokenService tokenService, ILogger<ImprovedAuditingMiddleware> logger)
        {
            _next = next;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // ⭐ VERIFICAR SI DEBE SALTARSE LA AUDITORÍA
                if (ShouldSkipAuditing(context))
                {
                    await _next(context);
                    return;
                }

                // Procesar con auditoría
                await ProcessWithAuditing(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en auditoría: {Message}", ex.Message);
                await _next(context);
            }
        }

        private bool ShouldSkipAuditing(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                // Verificar atributo personalizado
                if (endpoint.Metadata.GetMetadata<SkipAuditingAttribute>() != null)
                    return true;

                // Verificar AllowAnonymous
                if (endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
                    return true;
            }

            // Verificar rutas específicas
            var path = context.Request.Path.Value?.ToLower();
            var skipPaths = new[]
            {
                "/api/v1/ctabookingportal/public/",
                "/health",
                "/swagger",
                "/api/health"
            };

            return !string.IsNullOrEmpty(path) && skipPaths.Any(sp => path.StartsWith(sp));
        }

        private async Task ProcessWithAuditing(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var userId = _tokenService.GetClaimValue("sub");
                        var userEmail = _tokenService.GetClaimValue("email");
                        LogAuthenticatedRequest(context, userId, userEmail);
                    }
                    catch
                    {
                        LogAnonymousRequest(context);
                    }
                }
                else
                {
                    LogAnonymousRequest(context);
                }
            }
            else
            {
                LogAnonymousRequest(context);
            }

            await _next(context);
        }

        private void LogAuthenticatedRequest(HttpContext context, string? userId, string? userEmail)
        {
            _logger.LogInformation("Auth Request: {Method} {Path} by User {UserId} ({Email})",
                context.Request.Method,
                context.Request.Path,
                userId ?? "Unknown",
                userEmail ?? "Unknown");
        }

        private void LogAnonymousRequest(HttpContext context)
        {
            _logger.LogInformation("Anonymous Request: {Method} {Path} from {IP}",
                context.Request.Method,
                context.Request.Path,
                context.Connection.RemoteIpAddress?.ToString() ?? "Unknown");
        }
    }
}
