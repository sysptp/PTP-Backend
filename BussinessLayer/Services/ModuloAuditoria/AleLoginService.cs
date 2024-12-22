using AutoMapper;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Repository.ModuloAuditoria;
using BussinessLayer.Interfaces.Services.ModuloAuditoria;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Services.ModuloAuditoria
{
    public class AleLoginService : GenericService<AleLoginRequest, AleLoginReponse, AleLogin>, IAleLoginService
    {
        private readonly IAleLoginRepository _repository;
        private readonly IMapper _mapper;

        public AleLoginService(IAleLoginRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
