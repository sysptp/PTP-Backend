using BussinessLayer.DTOs.Configuracion.Seguridad.Permiso;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface IGnPermisoService : IGenericService<GnPermisoRequest, GnPermisoResponse,GnPermiso>
    {
        Task<List<GnPermisoResponse>> GetAllPermisosByFilter(long? companyId, int? roleId, int? menuId);

    }
}