using DataLayer.Models.HelpDesk;
using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Interfaces.Repository.HelpDesk;

namespace BussinessLayer.Services.SHelpDesk
{
    public class HdkSubCategoryService : GenericService<HdkSubCategoryRequest, HdkSubCategoryReponse, HdkSubCategory>, IHdkSubCategoryService
    {
        private readonly IHdkSubCategoryRepository _repository;
        private readonly IMapper _mapper;
        public HdkSubCategoryService(IHdkSubCategoryRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
