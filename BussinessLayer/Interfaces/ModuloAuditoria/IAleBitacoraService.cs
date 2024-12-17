using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloAuditoria;


namespace BussinessLayer.Interfaces.ModuloAuditoria
{
    public interface IAleBitacoraService : IGenericService<AleBitacoraRequest, AleBitacoraReponse, AleAuditoria>
    {
        Task AddAuditoria(AleBitacoraRequest vm);
        Task<List<AleBitacoraReponse>> GetAllByFilters(
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
