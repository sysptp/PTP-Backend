using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Empresa;

namespace BussinessLayer.Interfaces.IEmpresa
{
    public interface IGnSucursalService : IGenericService<GnSucursalRequest,GnSucursalResponse,GnSucursal>
    {

    }
}
