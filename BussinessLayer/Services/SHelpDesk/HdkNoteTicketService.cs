using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using BussinessLayer.DTOs.HelpDesk;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkNoteTicketService : GenericService<HdkNoteTicketRequest, HdkNoteTicketReponse, HdkNoteTicket>, IHdkNoteTicketService
    {
        private readonly IHdkNoteTicketRepository _repository;
        private readonly IMapper _mapper;
        public HdkNoteTicketService (IHdkNoteTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
