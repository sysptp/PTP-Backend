using AutoMapper;
using DataLayer.Models.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;

namespace BussinessLayer.Services.ModuloHelpDesk
{
    public class HdkDepartamentsService : GenericService<HdkDepartamentsRequest, HdkDepartamentsReponse, HdkDepartaments>, IHdkDepartamentsService
    {
        private readonly IHdkDepartamentsRepository _repository;
        private readonly IMapper _mapper;

        public HdkDepartamentsService(IHdkDepartamentsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
