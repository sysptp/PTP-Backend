using DataLayer.PDbContex;
using DataLayer.Models.Empresa;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.DTOs.Empresas;
using BussinessLayer.Interfaces.Repositories;
using AutoMapper;

namespace BussinessLayer.Services.SEmpresa
{
    public class SC_EMP001service : GenericService<SaveSC_EMP001Dto, SC_EMP001Dto, SC_EMP001>, ISC_EMP001service
    {
        public SC_EMP001service(IGenericRepository<SC_EMP001> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
