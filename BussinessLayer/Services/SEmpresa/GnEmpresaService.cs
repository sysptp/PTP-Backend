using DataLayer.Models.Empresa;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.DTOs.Empresas;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Empresa;

namespace BussinessLayer.Services.SEmpresa
{
    public class GnEmpresaservice : GenericService<GnEmpresaRequest, GnEmpresaResponse, GnEmpresa>, IGnEmpresaservice
    {
        private readonly IGnEmpresaRepository _repository;
        private readonly IMapper _mapper;
        public GnEmpresaservice(IGnEmpresaRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GnEmpresaResponse> GetByCodEmp(long id)
        {
           var empresa = await _repository.GetByEmpCode(id);
            return _mapper.Map<GnEmpresaResponse>(empresa);
        }
    }
}
