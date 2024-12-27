using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Interfaces.Services.ModuloAuditoria
{
    public interface IAleAuditTableControlService : IGenericService<AleAuditTableControlRequest, AleAuditTableControlResponse, AleAuditTableControl>
    {
    }
}
