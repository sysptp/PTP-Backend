using DataLayer.PDbContex;
using DataLayer.Models.Empresa;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.DTOs.Empresas;
using BussinessLayer.Interfaces.Repositories;
using AutoMapper;
using System.Runtime.CompilerServices;
using BussinessLayer.Interfaces.Repository.Empresa;

namespace BussinessLayer.Services.SEmpresa
{
    public class SC_EMP001service : GenericService<SaveSC_EMP001Dto, SC_EMP001Dto, SC_EMP001>, ISC_EMP001service
    {
        private readonly ISC_EMP001Repository _repository;
        private readonly IMapper _mapper;
        public SC_EMP001service(ISC_EMP001Repository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SC_EMP001Dto> GetByCodEmp(long id)
        {
           var empresa = await _repository.GetByEmpCode(id);
            return _mapper.Map<SC_EMP001Dto>(empresa);
        }
    }
}
