using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;
using BussinessLayer.Interfaces.Services.ModuloHelpDesk;

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
