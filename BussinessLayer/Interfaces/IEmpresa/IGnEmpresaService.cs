
using BussinessLayer.DTOs.Empresas;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Empresa;

namespace BussinessLayer.Interfaces.IEmpresa
{
    public interface IGnEmpresaservice : IGenericService<SaveGnEmpresaDto, GnEmpresaDto, GnEmpresa>
    {
        Task<GnEmpresaDto> GetByCodEmp(long id);
    }
}
