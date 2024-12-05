using AutoMapper;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
{
    public class HdkNoteTicketService : GenericService<HdkNoteTicketRequest, HdkNoteTicketReponse, HdkNoteTicket>, IHdkNoteTicketService
    {
        private readonly IHdkNoteTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkNoteTicketService(IHdkNoteTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
