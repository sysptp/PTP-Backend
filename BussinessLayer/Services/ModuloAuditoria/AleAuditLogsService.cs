using AutoMapper;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloAuditoria;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Services.ModuloAuditoria
{
    public class AleAuditLogsService : GenericService<AleAuditLogsRequest, AleAuditLogsResponse, AleAuditLogs>, IAleAuditLogsService
    {
        public AleAuditLogsService(IGenericRepository<AleAuditLogs> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
