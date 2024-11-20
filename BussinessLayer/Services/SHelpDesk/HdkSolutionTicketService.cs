using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Interfaces.Repository.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkSolutionTicketService : GenericService<HdkSolutionTicketRequest, HdkSolutionTicketReponse, HdkSolutionTicket>, IHdkSolutionTicketService
    {
        private readonly IHdkSolutionTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkSolutionTicketService(IHdkSolutionTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
