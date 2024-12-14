using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloAuditoria;


namespace BussinessLayer.Interfaces.ModuloAuditoria
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
