using AutoMapper;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
{
    public class HdkTicketsService : GenericService<HdkTicketsRequest, HdkTicketsReponse, HdkTickets>, IHdkTicketsService
    {
        private readonly IHdkTicketsRepository _repository;
        private readonly IMapper _mapper;
        public HdkTicketsService(IHdkTicketsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
