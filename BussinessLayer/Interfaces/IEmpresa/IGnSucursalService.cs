using BussinessLayer.DTOs.Sucursal;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models;
using DataLayer.Models.Empresa;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IEmpresa
{
    public interface IGnSucursalService : IGenericService<GnSucursalRequest,GnSucursalResponse,GnSucursal>
    {
        //Task<List<GnSucursal>> GetAllIndex();
        Task<GnSucursalResponse> GetBySucursalCode(long? id);
    }
}
