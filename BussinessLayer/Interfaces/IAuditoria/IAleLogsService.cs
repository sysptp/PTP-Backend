using BussinessLayer.DTOs.Auditoria;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Auditoria;

namespace BussinessLayer.Interfaces.IAuditoria
{
    public interface IAleLogsService : IGenericService<AleLogsRequest, AleLogsReponse, AleLogs>
    {
    }
}
