using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Interfaces.ModuloAuditoria
{
    public interface IAlePrintService : IGenericService<AlePrintRequest, AlePrintReponse, AlePrint>
    {
    }
}
