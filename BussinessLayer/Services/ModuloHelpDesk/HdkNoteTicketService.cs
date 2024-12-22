using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;
using BussinessLayer.Interfaces.Services.ModuloHelpDesk;

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
