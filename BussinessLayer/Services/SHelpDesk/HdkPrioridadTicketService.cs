using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Repository.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkPrioridadTicketService : GenericService<HdkPrioridadTicketRequest, HdkPrioridadTicketReponse, HdkPrioridadTicket>, IHdkPrioridadTicketService
    {
        private readonly HdkPrioridadTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkPrioridadTicketService(HdkPrioridadTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
