using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Seguridad.Permiso;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Services.SSeguridad.Permiso
{
    public class GnPermisoService : GenericService<GnPermisoRequest, GnPermisoResponse, GnPermiso>, IGnPermisoService
    {
        public GnPermisoService(IGenericRepository<GnPermiso> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
