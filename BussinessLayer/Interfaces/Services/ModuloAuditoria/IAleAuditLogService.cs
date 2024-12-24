using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Interfaces.Services.ModuloAuditoria
{
    public interface IAleAuditLogService : IGenericService<AleAuditLogRequest, AleAuditLogResponse, AleAuditLog>
    {
        Task<List<AleAuditLogResponse>> GetAllByFilter(
            string? tableName,
            string? action,
            string? oldValue,
            string? newValue,
            string? auditDate,
            long? idEmpresa);
    }
}
