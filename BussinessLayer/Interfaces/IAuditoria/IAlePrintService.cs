using BussinessLayer.DTOs.Auditoria;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Auditoria;

namespace BussinessLayer.Interfaces.IAuditoria
{
    public interface IAlePrintService : IGenericService<AlePrintRequest, AlePrintReponse, AlePrint>
    {
    }
}
