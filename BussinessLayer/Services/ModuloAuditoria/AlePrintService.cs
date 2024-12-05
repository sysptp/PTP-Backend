using AutoMapper;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.ModuloAuditoria;
using BussinessLayer.Interfaces.Repository.Auditoria;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Services.ModuloAuditoria
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
