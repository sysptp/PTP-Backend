using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Seguridad.Permiso;
using BussinessLayer.Interfaces.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.Seguridad;
using BussinessLayer.Wrappers;
using DataLayer.Models.Seguridad;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace BussinessLayer.Services.ModuloGeneral.Seguridad
{
    public class GnPermisoService : GenericService<GnPermisoRequest, GnPermisoResponse, GnPermiso>, IGnPermisoService
    {
        private readonly IGnPermisoRepository _permisoRepository;
        private readonly IMapper _mapper;

        public GnPermisoService(IGnPermisoRepository permisoRepository, IMapper mapper) : base(permisoRepository, mapper)
        {
            _permisoRepository = permisoRepository;
            _mapper = mapper;
        }

        public async Task<List<GnPermisoResponse>> GetAllPermisosByFilter(long? companyId, int? roleId, int? menuId)
        {
            var permisos = await _permisoRepository.GetAllWithIncludeAsync(new List<string> { "GnMenu", "GnPerfil", "GnEmpresa" });

            var permisosResponse = permisos.Select(permiso =>
            {
                var permisoResponse = _mapper.Map<GnPermisoResponse>(permiso);
                permisoResponse.MenuName = permiso.GnMenu.Menu;
                permisoResponse.RoleName = permiso.GnPerfil.Name;
                permisoResponse.CompanyName = permiso.GnEmpresa.NOMBRE_EMP;
                return permisoResponse;
            }).ToList();

            if (companyId != null)
            {
                permisosResponse = permisosResponse.Where(x => x.CompanyId == companyId).ToList();
            }

            if (roleId != null)
            {
                permisosResponse = permisosResponse.Where(x => x.RoleId == roleId).ToList();
            }

            if (menuId != null)
            {
                permisosResponse = permisosResponse.Where(x => x.MenuId == menuId).ToList();
            }

            return permisosResponse;
        }

        public async Task<Response<object>> AddOrUpdatePermissionAsync(GnPermisoRequest permisoDto)
        {
            var permissionList = await GetAllDto();
            var permissionExists = permissionList.Where(x => x.MenuId == permisoDto.MenuId && x.RoleId == permisoDto.RoleId && x.CompanyId == permisoDto.CompanyId).FirstOrDefault();
            if (permissionExists == null)
            {
                var createdPermission = await Add(permisoDto);

                return Response<object>.Success(createdPermission, "Permiso creado exitosamente");
            }
            else
            {
                permisoDto.IDPermiso = permissionExists.PermisoId;
                await Update(permisoDto, permissionExists.PermisoId);

                return Response<object>.Success(null, "Permiso actualizado correctamente");
            }
        }


    }
}
