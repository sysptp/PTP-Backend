using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.Interfaces.ModuloGeneral.Empresas;
using DataLayer.Models.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;

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
