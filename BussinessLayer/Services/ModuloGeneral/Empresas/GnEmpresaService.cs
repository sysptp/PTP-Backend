using DataLayer.Models.Empresa;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.Interfaces.ModuloGeneral.Empresas;

namespace BussinessLayer.Services.ModuloGeneral.Empresas
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

    }
}
