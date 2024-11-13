using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Seguridad.Permiso;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.Seguridad;
using DataLayer.Models.Seguridad;
using Microsoft.Identity.Client;

namespace BussinessLayer.Services.SSeguridad.Permiso
{
    public class GnPermisoService : GenericService<GnPermisoRequest, GnPermisoResponse, GnPermiso>, IGnPermisoService
    {
        private readonly IGnPermisoRepository _permisoRepository;
        private readonly IMapper _mapper;

        public GnPermisoService(IGnPermisoRepository permisoRepository, IMapper mapper) : base(permisoRepository,mapper)
        {
            _permisoRepository = permisoRepository;
            _mapper = mapper;
        }

        public async Task<List<GnPermisoResponse>> GetAllPermisosByFilter(long? companyId, int? roleId, int? menuId)
        {
            var permisos = await _permisoRepository.GetAllWithIncludeAsync(new List<string> { "GnMenu", "GnPerfil", "GnEmpresa" });
           var permisosResponse = new List<GnPermisoResponse>();
            foreach (var permiso in permisos)
            {
                var permisoResponse = _mapper.Map<GnPermisoResponse>(permiso);
                permisoResponse.MenuName = permiso.GnMenu.Menu;
                permisoResponse.RoleName = permiso.GnPerfil.Name;
                permisoResponse.CompanyName = permiso.GnEmpresa.NOMBRE_EMP;

                permisosResponse.Add(permisoResponse);
            }

              if (companyId != null)
            {
                permisosResponse = permisosResponse.Where(x => x.CompanyId == companyId).ToList();
            }

            if(roleId != null)
            {
                permisosResponse = permisosResponse.Where(x => x.RoleId == roleId).ToList();
            }

            if(menuId != null)
            {
                permisosResponse = permisosResponse.Where(x => x.MenuId == menuId).ToList();
            }

            return permisosResponse;
        }

    }
}
