using BussinessLayer.DTOs.Auditoria;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Auditoria;


namespace BussinessLayer.Interfaces.IAuditoria
{
    public interface IAleAuditoriaService : IGenericService<AleAuditoriaRequest, AleAuditoriaReponse, AleAuditoria>
    {
        Task AddAuditoria(AleAuditoriaRequest vm);
        Task<List<AleAuditoriaReponse>> GetAllByFilters(
            string modulo,
            string accion,
            int ano,
            int mes,
            int dia,
            int hora,
            string requestLike,
            string responseLike,
            string rolUsuario,
            long idEmpresa,
            long idSucursal);
    }
}
