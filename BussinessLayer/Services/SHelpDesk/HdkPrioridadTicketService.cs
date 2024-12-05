using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkPrioridadTicketService : GenericService<HdkPrioridadTicketRequest, HdkPrioridadTicketReponse, HdkPrioridadTicket>, IHdkPrioridadTicketService
    {
        private readonly IHdkPrioridadTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkPrioridadTicketService(IHdkPrioridadTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
