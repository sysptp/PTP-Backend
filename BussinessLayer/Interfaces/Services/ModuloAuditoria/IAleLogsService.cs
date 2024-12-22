using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Interfaces.Services.ModuloAuditoria
{
    public interface IAleLogsService : IGenericService<AleLogsRequest, AleLogsReponse, AleLogs>
    {
    }
}
