using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;
using BussinessLayer.Interfaces.Services.ModuloHelpDesk;

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
