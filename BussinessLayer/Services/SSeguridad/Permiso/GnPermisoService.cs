using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Seguridad.Permiso;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Seguridad;
using Microsoft.Identity.Client;

namespace BussinessLayer.Services.SSeguridad.Permiso
{
    public class GnPermisoService : GenericService<GnPermisoRequest, GnPermisoResponse, GnPermiso>, IGnPermisoService
    {
        public GnPermisoService(IGenericRepository<GnPermiso> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<GnPermisoResponse>> GetAllPermisosByFilter(long? companyId, int? roleId, int? menuId)
        {
            var permisos = await GetAllDto();
            if (companyId != null)
            {
                permisos = permisos.Where(x => x.CompanyId == companyId).ToList();
            }

            if(roleId != null)
            {
                permisos = permisos.Where(x => x.RoleId == roleId).ToList();
            }

            if(menuId != null)
            {
                permisos = permisos.Where(x => x.MenuId == menuId).ToList();
            }

            return permisos;
        }

    }
}
