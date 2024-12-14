using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloGeneral.Empresa;

namespace BussinessLayer.Interfaces.ModuloGeneral.Empresas
{
    public interface IGnEmpresaservice : IGenericService<GnEmpresaRequest, GnEmpresaResponse, GnEmpresa>
    {
    }
}
