
using BussinessLayer.DTOs.Empresas;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Empresa;

namespace BussinessLayer.Interfaces.IEmpresa
{
    public interface ISC_EMP001service : IGenericService<SaveSC_EMP001Dto, SC_EMP001Dto, SC_EMP001>
    {
        Task<SC_EMP001Dto> GetByCodEmp(long id);
    }
}
