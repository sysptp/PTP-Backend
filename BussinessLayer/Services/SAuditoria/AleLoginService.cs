using AutoMapper;
using BussinessLayer.DTOs.Auditoria;
using DataLayer.Models.Auditoria;
using BussinessLayer.Interfaces.IAuditoria;
using BussinessLayer.Interfaces.Repository.Auditoria;

namespace BussinessLayer.Services.SAuditoria
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
