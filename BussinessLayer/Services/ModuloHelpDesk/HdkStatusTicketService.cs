using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;
using BussinessLayer.Interfaces.Services.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
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
