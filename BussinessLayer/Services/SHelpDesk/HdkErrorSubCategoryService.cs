using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Repository.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkErrorSubCategoryService : GenericService<HdkErrorSubCategoryRequest, HdkErrorSubCategoryReponse, HdkErrorSubCategory>, IHdkErrorSubCategoryService
    {
        private readonly HdkErrorSubCategoryRepository _repository;
        private readonly IMapper _mapper;

        public HdkErrorSubCategoryService(HdkErrorSubCategoryRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
