using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.Empresa;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Empresas
{
    public interface IGnEmpresaservice : IGenericService<GnEmpresaRequest, GnEmpresaResponse, GnEmpresa>
    {
        Task<CompanyRegistrationResponse> RegisterCompanyWithAdmin(CompanyRegistrationRequest request);
    }
}
