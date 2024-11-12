using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Repository.HelpDesk;
using AutoMapper;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkNoteTicketService : GenericService<HdkNoteTicketRequest, HdkNoteTicketReponse, HdkNoteTicket>, IHdkNoteTicketService
    {
        private readonly HdkNoteTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkNoteTicketService (HdkNoteTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
