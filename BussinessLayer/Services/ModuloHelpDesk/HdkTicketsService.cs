using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;
using BussinessLayer.Interfaces.Services.ModuloHelpDesk;

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
