using AutoMapper;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
{
    public class HdkDepartXUsuarioService : GenericService<HdkDepartXUsuarioRequest, HdkDepartXUsuarioReponse, HdkDepartXUsuario>, IHdkDepartXUsuarioService
    {
        private readonly IHdkDepartXUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public HdkDepartXUsuarioService(IHdkDepartXUsuarioRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
