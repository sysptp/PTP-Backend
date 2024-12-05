using AutoMapper;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
{
    public class HdkTypeTicketService : GenericService<HdkTypeTicketRequest, HdkTypeTicketReponse, HdkTypeTicket>, IHdkTypeTicketService
    {
        private readonly IHdkTypeTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkTypeTicketService(IHdkTypeTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
