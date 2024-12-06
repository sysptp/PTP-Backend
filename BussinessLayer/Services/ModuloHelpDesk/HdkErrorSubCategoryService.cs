using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
{
    public class HdkErrorSubCategoryService : GenericService<HdkErrorSubCategoryRequest, HdkErrorSubCategoryReponse, HdkErrorSubCategory>, IHdkErrorSubCategoryService
    {
        private readonly IHdkErrorSubCategoryRepository _repository;
        private readonly IMapper _mapper;

        public HdkErrorSubCategoryService(IHdkErrorSubCategoryRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
