using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Permiso;
using BussinessLayer.Interfaces.IOtros;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Interfaces.ModuloGeneral.Seguridad
{
    public interface IGnPermisoService : IGenericService<GnPermisoRequest, GnPermisoResponse, GnPermiso>
    {
        Task<List<GnPermisoResponse>> GetAllPermisosByFilter(long? companyId, int? roleId, int? menuId);
        Task<Response<object>> AddOrUpdatePermissionAsync(GnPermisoRequest permisoDto);

    }
}