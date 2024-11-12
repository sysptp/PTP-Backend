using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Repository.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkTypeTicketService : GenericService<HdkTypeTicketRequest, HdkTypeTicketReponse, HdkTypeTicket>, IHdkTypeTicketService
    {
        private readonly HdkTypeTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkTypeTicketService(HdkTypeTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
