using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Services.SHelpDesk
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
