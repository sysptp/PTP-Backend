using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Permiso;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloGeneral.Seguridad;

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

        public async Task<List<GnPermisoResponse>> GetAllPermisosByFilter(long? companyId, int? roleId, int? menuId = null)
        {
            var permisos = await _permisoRepository.GetAllWithIncludeAsync(new List<string> { "GnMenu", "GnPerfil", "GnEmpresa" });

            var permisosResponse = permisos
                .Where(permiso => (!companyId.HasValue || permiso.Codigo_EMP == companyId) &&
                                  (!roleId.HasValue || permiso.IDPerfil == roleId) &&
                                  (!menuId.HasValue || permiso.IDMenu == menuId))
                .Select(permiso =>
                {
                    var permisoResponse = _mapper.Map<GnPermisoResponse>(permiso);
                    permisoResponse.MenuName = permiso.GnMenu.Menu;
                    permisoResponse.RoleName = permiso.GnPerfil.Name;
                    permisoResponse.CompanyName = permiso.GnEmpresa.NOMBRE_EMP;
                    return permisoResponse;
                })
                .ToList();

            return permisosResponse;
        }

        public async Task<List<GnPermisoResponseForLoggin>> GetAllPermisosForLogin(long? companyId, int? roleId, int? menuId = null)
        {
            var permisos = _mapper.Map<List<GnPermisoResponseForLoggin>>(await GetAllPermisosByFilter(companyId,roleId,menuId));

            return permisos;
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
