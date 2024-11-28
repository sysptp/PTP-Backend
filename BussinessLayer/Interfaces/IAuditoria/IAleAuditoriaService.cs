using BussinessLayer.DTOs.Auditoria;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Auditoria;


namespace BussinessLayer.Interfaces.IAuditoria
{
    public interface IAleAuditoriaService : IGenericService<AleAuditoriaRequest, AleAuditoriaReponse, AleAuditoria>
    {
    }
}
