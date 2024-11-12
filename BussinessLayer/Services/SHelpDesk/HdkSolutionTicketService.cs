using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Repository.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkSolutionTicketService : GenericService<HdkSolutionTicketRequest, HdkSolutionTicketReponse, HdkSolutionTicket>, IHdkSolutionTicketService
    {
        private readonly HdkSolutionTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkSolutionTicketService(HdkSolutionTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
