using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Services.SHelpDesk
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
