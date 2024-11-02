using DataLayer.Models.Empresa;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.DTOs.Empresas;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Empresa;

namespace BussinessLayer.Services.SEmpresa
{
    public class GnEmpresaservice : GenericService<SaveGnEmpresaDto, GnEmpresaDto, GnEmpresa>, IGnEmpresaservice
    {
        private readonly IGnEmpresaRepository _repository;
        private readonly IMapper _mapper;
        public GnEmpresaservice(IGnEmpresaRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GnEmpresaDto> GetByCodEmp(long id)
        {
           var empresa = await _repository.GetByEmpCode(id);
            return _mapper.Map<GnEmpresaDto>(empresa);
        }
    }
}
