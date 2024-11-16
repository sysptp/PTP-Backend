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

        public async Task<List<HdkCategoryTicketReponse>> GetAllWithInclude()
        {
            var categories = await _repository.GetAllWithIncludeAsync(new List<string> {"GnEmpresa"});
            var categoriesResponse = new List<HdkCategoryTicketReponse>();

            foreach (var category in categories) 
            { 
                var response = _mapper.Map<HdkCategoryTicketReponse>(category);
                response.NombreEmpresa = category.GnEmpresa.NOMBRE_EMP;

                categoriesResponse.Add(response);
            }

            return categoriesResponse;
        }
    }
}
