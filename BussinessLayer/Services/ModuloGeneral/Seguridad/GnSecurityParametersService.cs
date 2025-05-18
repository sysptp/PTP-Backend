using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.GnSecurityParameters;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Services.ModuloGeneral.Seguridad
{
    public class GnSecurityParametersService : GenericService<GnSecurityParametersRequest, GnSecurityParametersResponse, GnSecurityParameters>, IGnSecurityParametersService
    {
        public GnSecurityParametersService(IGnSecurityParametersRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
