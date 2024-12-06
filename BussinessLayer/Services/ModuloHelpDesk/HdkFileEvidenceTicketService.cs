using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
{
    public class HdkFileEvidenceTicketService : GenericService<HdkFileEvidenceTicketRequest, HdkFileEvidenceTicketReponse, HdkFileEvidenceTicket>, IHdkFileEvidenceTicketService
    {
        private readonly IHdkFileEvidenceTicketRepository _repository;
        private readonly IMapper _mapper;

        public HdkFileEvidenceTicketService(IHdkFileEvidenceTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
