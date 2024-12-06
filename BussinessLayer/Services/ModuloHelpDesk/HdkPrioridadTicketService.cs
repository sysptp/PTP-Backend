using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;

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
