using AutoMapper;
using BussinessLayer.DTOs.Auditoria;
using DataLayer.Models.Auditoria;
using BussinessLayer.Interfaces.IAuditoria;
using BussinessLayer.Interfaces.Repository.Auditoria;

namespace BussinessLayer.Services.SAuditoria
{
    public class AleLogsService : GenericService<AleLogsRequest, AleLogsReponse, AleLogs>, IAleLogsService
    {
        private readonly IAleLogsRepository _repository;
        private readonly IMapper _mapper;

        public AleLogsService(IAleLogsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
