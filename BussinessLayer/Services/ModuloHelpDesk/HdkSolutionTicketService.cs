using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
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
