using AutoMapper;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
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
