using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Interfaces.Repository.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkTicketsService : GenericService<HdkTicketsRequest, HdkTicketsReponse, HdkTickets>, IHdkTicketsService
    {
        private readonly IHdkTicketsRepository _repository;
        private readonly IMapper _mapper;
        public HdkTicketsService(IHdkTicketsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
