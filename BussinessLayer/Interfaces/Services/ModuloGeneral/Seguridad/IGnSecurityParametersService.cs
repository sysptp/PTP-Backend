using BussinessLayer.DTOs.ModuloGeneral.Seguridad.GnSecurityParameters;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad
{
    public interface IGnSecurityParametersService : IGenericService<GnSecurityParametersRequest, GnSecurityParametersResponse, GnSecurityParameters>
    {
    }
}
