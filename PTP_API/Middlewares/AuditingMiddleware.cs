using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Services.ModuloAuditoria;
using System.Text;

namespace PTP_API.Middlewares
{
    public class AuditingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public AuditingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            var endpoint = context.GetEndpoint();
            var isAuditable = endpoint?.Metadata.GetMetadata<EnableBitacoraAttribute>() != null;
            var isDisableBitacora = endpoint?.Metadata.GetMetadata<DisableBitacoraAttribute>() != null;

            if (!isAuditable || isDisableBitacora)
            {
                await _next(context);
                return;
            }

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                var requestBody = string.Empty;

                if (context.Request.Body.CanSeek)
                {
                    context.Request.Body.Position = 0;
                    using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                    requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                var tokenService = context.RequestServices.GetRequiredService<ITokenService>();

                var userName = tokenService.GetClaimValue("sub");

                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var auditLog = new AleBitacoraRequest
                {
                    Modulo = "API",
                    Acccion = $"{context.Request.Method} {context.Request.Path}",
                    Request = requestBody,
                    Response = responseBodyText,
                    IP = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                    UserName = userName,
                };

                _ = Task.Run(async () =>
                {
                    try
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var auditService = scope.ServiceProvider.GetRequiredService<IAleBitacoraService>();
                        await auditService.AddAuditoria(auditLog);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error al guardar la auditoría: {ex.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error en el middleware de auditoría: {ex.Message}");
                throw;
            }
            finally
            {
                await responseBody.CopyToAsync(originalBodyStream);
                context.Response.Body = originalBodyStream;
            }
        }
    }
}