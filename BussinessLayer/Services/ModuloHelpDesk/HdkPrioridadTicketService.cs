using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;
using BussinessLayer.Interfaces.Services.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
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
