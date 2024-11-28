using AutoMapper;
using BussinessLayer.DTOs.Auditoria;
using DataLayer.Models.Auditoria;
using BussinessLayer.Interfaces.IAuditoria;
using BussinessLayer.Interfaces.Repository.Auditoria;

namespace BussinessLayer.Services.SAuditoria
{
    public class AlePrintService : GenericService<AlePrintRequest, AlePrintReponse, AlePrint>, IAlePrintService
    {
        private readonly IAlePrintRepository _repository;
        private readonly IMapper _mapper;

        public AlePrintService(IAlePrintRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
