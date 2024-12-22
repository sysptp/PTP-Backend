using AutoMapper;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloAuditoria;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Services.ModuloAuditoria
{
    public class AleAuditTableControlService : GenericService<AleAuditTableControlRequest, AleAuditTableControlResponse, AleAuditTableControl>, IAleAuditTableControlService
    {
        public AleAuditTableControlService(IGenericRepository<AleAuditTableControl> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
