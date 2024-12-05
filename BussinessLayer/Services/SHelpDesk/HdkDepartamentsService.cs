using BussinessLayer.Interfaces.IHelpDesk;
using AutoMapper;
using BussinessLayer.Interfaces.Repository.HelpDesk;
using BussinessLayer.DTOs.HelpDesk;
using DataLayer.Models.ModuloHelpDesk;

namespace BussinessLayer.Services.SHelpDesk
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
