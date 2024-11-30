using BussinessLayer.DTOs.Auditoria;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Auditoria;


namespace BussinessLayer.Interfaces.IAuditoria
{
    public interface IAleLoginService : IGenericService<AleLoginRequest, AleLoginReponse, AleLogin>
    {
    }
}
