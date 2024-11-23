using AutoMapper;
using BussinessLayer.DTOs.Auditoria;
using DataLayer.Models.Auditoria;
using BussinessLayer.Interfaces.IAuditoria;
using BussinessLayer.Interfaces.Repository.Auditoria;

namespace BussinessLayer.Services.SAuditoria
{
    public class AleAuditoriaService : GenericService<AleAuditoriaRequest, AleAuditoriaReponse, AleAuditoria>, IAleAuditoriaService
    {
        private readonly IAleAuditoriaRepository _repository;
        private readonly IMapper _mapper;

        public AleAuditoriaService(IAleAuditoriaRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
