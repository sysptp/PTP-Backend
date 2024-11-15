using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Interfaces.Repository.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkStatusTicketService : GenericService<HdkStatusTicketRequest, HdkStatusTicketReponse, HdkStatusTicket>, IHdkStatusTicketService
    {
        private readonly IHdkStatusTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkStatusTicketService(IHdkStatusTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
