using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using BussinessLayer.DTOs.HelpDesk;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Services.SHelpDesk
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
