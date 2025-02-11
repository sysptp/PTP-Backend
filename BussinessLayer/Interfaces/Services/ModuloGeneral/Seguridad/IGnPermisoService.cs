using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Permiso;
using BussinessLayer.Interfaces.Services.IOtros;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad
{
    public interface IGnPermisoService : IGenericService<GnPermisoRequest, GnPermisoResponse, GnPermiso>
    {
        Task<List<GnPermisoResponse>> GetAllPermisosByFilter(long? companyId, int? roleId, int? menuId = null);
        Task<Response<object>> AddOrUpdatePermissionAsync(GnPermisoRequest permisoDto);

    }
}