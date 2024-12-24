using AutoMapper;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.ModuloAuditoria;
using BussinessLayer.Interfaces.Services.ModuloAuditoria;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Services.ModuloAuditoria
{
    public class AleAuditLogService : GenericService<AleAuditLogRequest, AleAuditLogResponse, AleAuditLog>, IAleAuditLogService
    {
        private readonly IAleAuditLogRepository _repository;
        private readonly IMapper _mapper;

        public AleAuditLogService(IAleAuditLogRepository repository, IMapper mapper) : base(repository,mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Método para obtener una lista de logs de auditoría filtrada por diversos criterios.
        /// Este método permite aplicar filtros opcionales como el nombre de la tabla, acción,
        /// valores antiguos y nuevos, fecha de auditoría y el ID de la empresa.
        /// </summary>
        /// <param name="tableName">
        /// Nombre de la tabla involucrada en la acción. Este filtro es opcional.
        /// </param>
        /// <param name="action">
        /// Acción realizada, como "INSERT", "UPDATE", "DELETE". Este filtro es opcional.
        /// </param>
        /// <param name="oldValue">
        /// Valor antiguo antes de realizar la acción. Este filtro es opcional.
        /// </param>
        /// <param name="newValue">
        /// Valor nuevo después de realizar la acción. Este filtro es opcional.
        /// </param>
        /// <param name="auditDate">
        /// Fecha de la auditoría en formato "yyyy-MM-dd". Este filtro es opcional.
        /// </param>
        /// <param name="idEmpresa">
        /// ID de la empresa asociada al log de auditoría. Este filtro es opcional.
        /// </param>
        /// <returns>
        /// Una tarea que representa la operación asincrónica y devuelve una lista de objetos
        /// <see cref="AleAuditLogResponse"/> que cumplen con los criterios de filtrado.
        /// </returns>
        public async Task<List<AleAuditLogResponse>> GetAllByFilter(
            string? tableName,
            string? action,
            string? oldValue,
            string? newValue,
            string? auditDate,
            long? idEmpresa)
        {
            var logs = await GetAllDto();
            var query = logs.AsQueryable();

            if (!string.IsNullOrEmpty(tableName))
                query = query.Where(x => x.TableName.Contains(tableName));

            if (!string.IsNullOrEmpty(action))
                query = query.Where(x => x.Action.Contains(action));

            if (!string.IsNullOrEmpty(oldValue))
                query = query.Where(x => x.OldValue.Contains(oldValue));

            if (!string.IsNullOrEmpty(newValue))
                query = query.Where(x => x.NewValue.Contains(newValue));

            if (!string.IsNullOrEmpty(auditDate) && DateTime.TryParse(auditDate, out var parsedDate))
                query = query.Where(x => x.AuditDate.Date == parsedDate.Date);

            if (idEmpresa.HasValue && idEmpresa > 0)
                query = query.Where(x => x.IdEmpresa == idEmpresa);

            return query.ToList();
        }

    }
}
