using AutoMapper;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.ModuloAuditoria;
using BussinessLayer.Interfaces.Repository.ModuloAuditoria;
using DataLayer.Models.ModuloAuditoria;

namespace BussinessLayer.Services.ModuloAuditoria
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
