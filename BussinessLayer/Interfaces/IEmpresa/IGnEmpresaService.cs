
using BussinessLayer.DTOs.Empresas;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Empresa;

namespace BussinessLayer.Interfaces.IEmpresa
{
    public interface IGnEmpresaservice : IGenericService<GnEmpresaRequest, GnEmpresaResponse, GnEmpresa>
    {
    }
}
