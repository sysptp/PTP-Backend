using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Repository.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkStatusTicketService : GenericService<HdkStatusTicketRequest, HdkStatusTicketReponse, HdkStatusTicket>, IHdkStatusTicketService
    {
        private readonly HdkStatusTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkStatusTicketService(HdkStatusTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
