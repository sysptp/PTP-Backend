using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using BussinessLayer.DTOs.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkCategoryTicketService : GenericService<HdkCategoryTicketRequest, HdkCategoryTicketReponse, HdkCategoryTicket>, IHdkCategoryTicketService
    {
        private readonly IHdkCategoryTicketRepository _repository;
        private readonly IMapper _mapper;

        public HdkCategoryTicketService (IHdkCategoryTicketRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
